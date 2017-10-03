using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using SucLib.Model;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.PanGu;

namespace Lucence.Net
{
    public partial class _Default : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        public List<SUC_NEWS> modResult = new List<SUC_NEWS>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string btnCreate = Request.QueryString["btnCreate"];
            string btnSearch = Request.QueryString["btnSearch"];
            string btnSelf = Request.QueryString["btnSelf"];
            if (!string.IsNullOrEmpty(btnCreate))
            {
                //创建索引库
                CreateIndexByData();
            }
            if (!string.IsNullOrEmpty(btnSearch))
            {
                //搜索
                SearchFromIndexData(Request.QueryString["SearchKey"]);
                BindData(1);
            }
            if (!string.IsNullOrEmpty(btnSelf))
            {
                //搜索
                SearchFromIndexSelf();
                BindData(1);
            }
        }

        private void BindData(int pageNum)
        {
            rp1.DataSource = modResult.Skip(10 * (pageNum - 1)).Take(10);   //10 pagesize
            rp1.DataBind();
        }

        private void SearchFromIndexSelf()
        {
            int max = new SUC_NEWS().MaxID();
            int rand = new Random().Next(max);
            string name = new SUC_NEWS().FindByCondition(new SUC_NEWS() { ID = rand })[0].TITLE;
            string[] titles = SplitContent.SplitWords(name);
            rand = new Random().Next(titles.Length);
            SearchFromIndexData(titles[rand]);
        }

        private void CreateIndexByData()
        {
            string indexPath = Context.Server.MapPath("~/IndexData");//索引文档保存位置

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            //IndexReader:对索引库进行读取的类
            bool isExist = IndexReader.IndexExists(directory); //是否存在索引库文件夹以及索引库特征文件
            if (isExist)
            {
                //如果索引目录被锁定（比如索引过程中程序异常退出或另一进程在操作索引库），则解锁
                //Q:存在问题 如果一个用户正在对索引库写操作 此时是上锁的 而另一个用户过来操作时 将锁解开了 于是产生冲突 --解决方法后续
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            if (System.IO.Directory.Exists(indexPath))
            {
                DirectoryInfo di = new DirectoryInfo(indexPath);
                foreach (System.IO.FileInfo f in di.GetFiles())
                {
                    f.Delete();
                }
            }

            //创建向索引库写操作对象  IndexWriter(索引目录,指定使用盘古分词进行切词,最大写入长度限制)
            //补充:使用IndexWriter打开directory时会自动对索引库文件上锁
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
            List<SUC_NEWS> news = new SUC_NEWS().FindAll();

            //--------------------------------遍历数据源 将数据转换成为文档对象 存入索引库
            foreach (var m in news)
            {
                Document document = new Document(); //new一篇文档对象 --一条记录对应索引库中的一个文档

                //向文档中添加字段  Add(字段,值,是否保存字段原始值,是否针对该列创建索引)
                document.Add(new Field("ID", m.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//--所有字段的值都将以字符串类型保存 因为索引库只存储字符串类型数据

                //Field.Store:表示是否保存字段原值。指定Field.Store.YES的字段在检索时才能用document.Get取出原值  
                //Field.Index.NOT_ANALYZED:指定不按照分词后的结果保存--是否按分词后结果保存取决于是否对该列内容进行模糊查询

                document.Add(new Field("TITLE", m.TITLE, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

                //Field.Index.ANALYZED:指定文章内容按照分词后结果保存 否则无法实现后续的模糊查询 
                //WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词 还保存词之间的距离

                document.Add(new Field("CONTENT", m.CONTENT, Field.Store.YES, Field.Index.NOT_ANALYZED));//Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

                document.Add(new Field("URL", m.pandaWebUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));
                writer.AddDocument(document); //文档写入索引库
            }
            writer.Close();//会自动解锁
            directory.Close(); //不要忘了Close，否则索引结果搜不到
            Response.Write("<script language='javascript'>alert('生成完毕');</script>");
        }

        private void SearchFromIndexData(string searchkey)
        {

            string indexPath = Context.Server.MapPath("~/IndexData");
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            PhraseQuery query = new PhraseQuery();
            //把用户输入的关键字进行分词
            foreach (string word in SplitContent.SplitWords(searchkey))
            {
                query.Add(new Term("TITLE", word));
            }
            //query.Add(new Term("content", "C#"));//多个查询条件时 为且的关系
            query.SetSlop(100); //指定关键词相隔最大距离

            //TopScoreDocCollector盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);//根据query查询条件进行查询，查询结果放入collector容器
            //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;//collector.GetTotalHits()

            //展示数据实体对象集合
            for (int i = 0; i < docs.Length; i++)
            {
                int docID = docs[i].doc;//得到查询结果文档的ID（Lucene内部分配的ID）
                Document doc = searcher.Doc(docID);//根据文档ID来获得文档对象Document
                SUC_NEWS mod = new SUC_NEWS();
                mod.TITLE = SplitContent.HightLight(searchkey, doc.Get("TITLE"));
                mod.TITLE = string.IsNullOrEmpty(mod.TITLE) ? doc.Get("TITLE") : mod.TITLE;
                //book.ContentDESCRPTION = doc.Get("content");//未使用高亮
                //搜索关键字高亮显示 使用盘古提供高亮插件
                mod.CONTENT = SplitContent.HightLight(searchkey, doc.Get("CONTENT"));
                mod.CONTENT = string.IsNullOrEmpty(mod.CONTENT) ? doc.Get("CONTENT") : mod.CONTENT;
                mod.CONTENT = mod.CONTENT.Replace("<b>", "");
                mod.ID = Convert.ToInt32(doc.Get("ID"));
                mod.pandaWebUrl = doc.Get("URL");
                modResult.Add(mod);
            }
        }



        protected LosFormatter losFormatter = new LosFormatter();
        /// <summary>
        /// 序列化所有视图状态信息和控件状态信息。
        /// </summary>
        /// <param name="viewState">要在其中存储视图状态信息的 Object</param>
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            string val = Request.Url + "__VIEWSTATE";
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            losFormatter.Serialize(stream, viewState);
            stream.Flush();
            Session[val] = stream;
        }
    }
}