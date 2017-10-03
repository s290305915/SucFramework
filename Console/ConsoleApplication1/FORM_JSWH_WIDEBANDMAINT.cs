using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class ONE
    {
        public List<FORM_JSWH_WIDEBANDMAINT> Data { get; set; }
    }

    public class TWO
    {
        public List<FORM_JSWH_WIDEBANDMAINT_WLAN> Data { get; set; }
    }

    public class THREE
    {
        public List<FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE> Data { get; set; }
    }


    //1
    public class FORM_JSWH_WIDEBANDMAINT
    {
        public string TASKID { get; set; }
        public string SN { get; set; }
        public string APPLYTIME { get; set; }
        public string APPLYUSER { get; set; }
        public string BILLSOURCE { get; set; }
        public string CITY { get; set; }
        public string VILLAGE { get; set; }
        public string AREAFAULT { get; set; }
        public string COMMUNITYTYPE { get; set; }
        public string COMMUNITYNAME { get; set; }
        public string CUSTOMERTEL { get; set; }
        public string DISTRICT { get; set; }
        public string RESPONSIBLEUNIT { get; set; }
        public string EMOSDATE { get; set; }
        public string EMOSDELYTIME { get; set; }
        public string FAULTTYPE { get; set; }
        public string CUSTOMERACCOUNT { get; set; }
        public string SYMPTOM { get; set; }
        public string LIMITTIME { get; set; }
        public string PRESTEP { get; set; }
        public string FAULTDETAIL { get; set; }
        public string DELYHOURS { get; set; }
        public string CALLBACKOPERATION { get; set; }
        public string RETURNBACKREASON { get; set; }
        public string RESONSFORFAILURE { get; set; }
        public string WEATHERACHIVED { get; set; }
        public string WORKPERMIT { get; set; }
        public string ATTITUDESATISFY { get; set; }
        public string RESULTSATISFY { get; set; }
        public string OTHERPROBLEMS { get; set; }
        public string OTHERPROBLEMDEAL { get; set; }
        public string VIEWFAILUREMARK { get; set; }
        public string UNCOVERREASON { get; set; }
    }

    //2
    public class FORM_JSWH_WIDEBANDMAINT_WLAN
    {
        public string TASKID { get; set; }
        public string SN { get; set; }
        public string APPLYTIME { get; set; }
        public string APPLYUSER { get; set; }
        public string BILLSOURCE { get; set; }
        public string WLANNAME { get; set; }
        public string CUSTOMERTEL { get; set; }
        public string BILLCLASS { get; set; }
        public string EOMSAPPLYTIME { get; set; }
        public string EOMSDELAYTIME { get; set; }
        public string DISTRICT { get; set; }
        public string RESPONSIBLEUNIT { get; set; }
        public string ROOMNUM { get; set; }
        public string LIMITTIME { get; set; }
        public string FAULTDETAIL { get; set; }
        public string DELYHOURS { get; set; }
        public string CALLBACKOPERATION { get; set; }
        public string RETURNBACKREASON { get; set; }
        public string RESONSFORFAILURE { get; set; }
        public string WEATHERACHIVED { get; set; }
        public string WORKPERMIT { get; set; }
        public string ATTITUDESATISFY { get; set; }
        public string RESULTSATISFY { get; set; }
        public string OTHERPROBLEMS { get; set; }
        public string OTHERPROBLEMDEAL { get; set; }
        public string VIEWFAILUREMARK { get; set; }
        public string UNCOVERREASON { get; set; }
    }


    //3
    public class FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE
    {
        public string TASKID { get; set; }
        public string SN { get; set; }
        public string APPLYTIME { get; set; }
        public string APPLYUSER { get; set; }
        public string LINENAME { get; set; }
        public string BILLSOURCE { get; set; }
        public string LINETYPE { get; set; }
        public string CUSTOMERTEL { get; set; }
        public string DISTRICT { get; set; }
        public string EOMSAPPLYTIME { get; set; }
        public string EOMSDELAYTIME { get; set; }
        public string COMMUNITYNAME { get; set; }
        public string RESPONSIBLEUNIT { get; set; }
        public string LIMITTIME { get; set; }
        public string ATTACHMENT { get; set; }
        public string FAULTDETAIL { get; set; }
        public string CALLBACKOPERATION { get; set; }
        public string WEATHERACHIVED { get; set; }
        public string ATTITUDESATISFY { get; set; }
        public string WORKPERMIT { get; set; }
        public string RESULTSATISFY { get; set; }
        public string OTHERPROBLEMDEAL { get; set; }
        public string DELYHOURS { get; set; }
    }
}
