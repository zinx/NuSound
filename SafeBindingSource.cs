using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ACT_Plugin
{
    class SafeBindingSource : BindingSource
    {
        public Control UIThreadMarshal { get; set; }
        private delegate void OnListChangedDelegate(ListChangedEventArgs e);
        private Dictionary<string, SafeBindingSource> relatedBindingSources  = new Dictionary<string, SafeBindingSource>();

        public SafeBindingSource()
            : base()
        {
        }

        public SafeBindingSource(IContainer components)
            : base(components)
        {
        }

        public SafeBindingSource(object dataSource, string dataMember)
            : base(dataSource, dataMember)
        {
        }

        public override CurrencyManager GetRelatedCurrencyManager(string dataMember)
        {
            CurrencyManager result = null;
            if (string.IsNullOrEmpty(dataMember))
            {
                //If you call the CurrencyManager property you end up in a recursive loop
                Type t = this.GetType().BaseType;
                result = t.InvokeMember("currencyManager", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null) as CurrencyManager;
            }
            else if (dataMember.IndexOf(".") != -1)
            {
                //dot notation is not supported by the BindingSource
                result = null;
            }
            else if (relatedBindingSources.ContainsKey(dataMember))
            {
                result = relatedBindingSources[dataMember].CurrencyManager;
            }
            else
            {
                SafeBindingSource bindingSource = new SafeBindingSource(this, dataMember);
                this.relatedBindingSources.Add(dataMember, bindingSource);
                result = bindingSource.CurrencyManager;
            }
            return result;
        }

        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
        {
            if (UIThreadMarshal != null && UIThreadMarshal.InvokeRequired)
            {
                OnListChangedDelegate changeDelegate = new OnListChangedDelegate(base.OnListChanged);
                UIThreadMarshal.Invoke(new MethodInvoker(delegate { OnListChanged(e); }));
            }
            else
            {
                base.OnListChanged(e);
            }
        }
    }
}
