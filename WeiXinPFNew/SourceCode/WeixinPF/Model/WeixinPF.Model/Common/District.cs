using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.Common
{
    /// <summary>
	/// 中国省市县镇4级数据表
	/// </summary>
	[Serializable]
    public partial class DistrictInfo
    {
        public DistrictInfo()
        { }
        #region Model
        private int _id;
        private string _name;
        private int? _level;
        private int? _upid;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? upid
        {
            set { _upid = value; }
            get { return _upid; }
        }
        #endregion Model

    }
}
