using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using SucLib.Model;
using SucLib.Data.IDal;
using SucLib.Data.Factory;

public class IndexManager
{
    public static readonly IndexManager bookIndex = new IndexManager();
    public static readonly string indexPath = HttpContext.Current.Server.MapPath("~/IndexData");
    private IndexManager()
    {
    }
    //请求队列 解决索引目录同时操作的并发问题
    private Queue<ModelViewMode> bookQueue = new Queue<ModelViewMode>();
    /// <summary>
    /// 新增models表信息时 添加新增索引请求至队列
    /// </summary>
    /// <param TITLE="models"></param>
    public void Add(SUC_NEWS news)
    {
        ModelViewMode mvm = new ModelViewMode();
        mvm.ID = news.ID;
        mvm.TITLE = news.TITLE;
        mvm.IT = IndexType.Insert;
        mvm.CONTENT = news.CONTENT;
        mvm.URL = news.pandaWebUrl;
        bookQueue.Enqueue(mvm);
    }
    /// <summary>
    /// 删除models表信息时 添加删除索引请求至队列
    /// </summary>
    /// <param TITLE="bID"></param>
    public void Del(int bID)
    {
        ModelViewMode mvm = new ModelViewMode();
        mvm.ID = bID;
        mvm.IT = IndexType.Delete;
        bookQueue.Enqueue(mvm);
    }
    /// <summary>
    /// 修改models表信息时 添加修改索引(实质上是先删除原有索引 再新增修改后索引)请求至队列
    /// </summary>
    /// <param TITLE="models"></param>
    public void Mod(SUC_NEWS news)
    {
        ModelViewMode mvm = new ModelViewMode();
        mvm.ID = news.ID;
        mvm.TITLE = news.TITLE;
        mvm.IT = IndexType.Modify;
        mvm.CONTENT = news.CONTENT;
        mvm.URL = news.pandaWebUrl;
        bookQueue.Enqueue(mvm);
    }

    public void StartNewThread()
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
    }

    //定义一个线程 将队列中的数据取出来 插入索引库中
    private void QueueToIndex(object para)
    {
        while (true)
        {
            if (bookQueue.Count > 0)
            {
                CRUDIndex();
            }
            else
            {
                Thread.Sleep(3000);
            }
        }
    }
    /// <summary>
    /// 更新索引库操作
    /// </summary>
    private void CRUDIndex()
    {
        FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
        bool isExist = IndexReader.IndexExists(directory);
        if (isExist)
        {
            if (IndexWriter.IsLocked(directory))
            {
                IndexWriter.Unlock(directory);
            }
        }
        IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
        while (bookQueue.Count > 0)
        {
            Document document = new Document();
            ModelViewMode model = bookQueue.Dequeue();
            if (model.IT == IndexType.Insert)
            {
                document.Add(new Field("ID", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("TITLE", model.TITLE, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("CONTENT", model.CONTENT, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("URL", model.URL, Field.Store.YES, Field.Index.NOT_ANALYZED));
                writer.AddDocument(document);
            }
            else if (model.IT == IndexType.Delete)
            {
                writer.DeleteDocuments(new Term("ID", model.ID.ToString()));
            }
            else if (model.IT == IndexType.Modify)
            {
                //先删除 再新增
                writer.DeleteDocuments(new Term("ID", model.ID.ToString()));
                document.Add(new Field("ID", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("TITLE", model.TITLE, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("CONTENT", model.CONTENT, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("URL", model.URL, Field.Store.YES, Field.Index.NOT_ANALYZED));
                writer.AddDocument(document);
            }
        }
        writer.Close();
        directory.Close();
    }
}

public class ModelViewMode
{
    public int ID
    {
        get;
        set;
    }
    public string TITLE
    {
        get;
        set;
    }
    public string CONTENT
    {
        get;
        set;
    }

    public string URL
    {
        get;
        set;
    }

    public IndexType IT
    {
        get;
        set;
    }
}
//操作类型枚举
public enum IndexType
{
    Insert,
    Modify,
    Delete
}