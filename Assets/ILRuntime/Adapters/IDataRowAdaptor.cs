using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Intepreter;
using System;
using ILRuntime.CLR.Method;
using GameFramework.DataTable;

public class IDataRowAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(IDataRow);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);//创建一个新的实例
    }

    internal class Adaptor : IDataRow, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        { }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }
        public ILTypeInstance ILInstance
        {
            get
            {
                return instance;
            }
        }

        object[] param1 = new object[1];

        IMethod _mParseDataRow;
        bool _isParseDataRow = false;

        public int Id
        {
            get;
            protected set;
        }
        
        public void ParseDataRow(string dataRowText)
        {
            if (!_isParseDataRow)
            {
                _mParseDataRow = instance.Type.GetMethod("ParseDataRow", 1);
                _isParseDataRow = true;
            }
            if (_mParseDataRow != null)
            {
                param1[0] = dataRowText;
                 appdomain.Invoke(this._mParseDataRow, instance, this.param1);
            }
        }
    }
}
