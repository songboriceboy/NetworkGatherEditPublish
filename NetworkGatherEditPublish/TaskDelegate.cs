using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace NetworkGatherEditPublish
{

    public class DelegatePara
    {
        public int nIsAdsComment;
        public string strLog;
        public string strTitle;
    }
    public class TaskDelegate
    {
        public TaskDelegate() { }
        public TaskDelegate(ccTaskDelegate refreshDele)
        {

            Refresh += refreshDele;

        }

        public ccTaskDelegate Refresh { get; set; }

    }
    public delegate void ccTaskDelegate(DelegatePara dp);




}
