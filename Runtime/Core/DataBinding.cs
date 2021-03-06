using System;
using System.Collections.Generic;
using UnityEngine;

namespace ParthViradiya.Core
{
    /// <summary>
    /// DataBindings are components which observe DataSources and can execute logic when they change.
    /// Each DataBinding should contain DataSourceReferences which allow for the binding of DataSources on specific ViewModels.
    /// The ViewModel for DataBindings can be explicitly set or can be located (by searching upwards from the
    /// DataBinding owner) using the 'Locate View Model' button in the inspector.
    ///
    /// By default, all bindings are unbound on both OnDisable and OnDestroy.
    /// You can change the `UnbindOnDisable` and `UnbindOnDestroy` flags to modify this behavior.
    /// </summary>
    [ExecuteInEditMode]
    public abstract class DataBinding : MonoBehaviour
    {
        private struct Entry
        {
            public DataSourceReference DataSourceReference;
            public EventHandler Handler;
            public EventHandler<CollectionChangedEventArgs> CollectionHandler;
            public Action UnbindCallback;
        }

        [Tooltip("Assign a object that type of ViewModel or ViewModelScriptable")]
        public UnityEngine.Object ViewModel;

        protected bool UnbindOnDisable = true;
        protected bool UnbindOnDestroy = true;

        private readonly List<Entry> bindings = new List<Entry>();

        protected virtual void OnEnable()
        {
            Rebind();
        }

        protected abstract void Setup();

        protected void Bind(DataSourceReference dataSourceReference, EventHandler handler, Action unbindCallback = null)
        {
            if (dataSourceReference?.Source == null) return;
            bindings.Add(new Entry
            {
                DataSourceReference = dataSourceReference,
                Handler = handler,
                UnbindCallback = unbindCallback
            });

            dataSourceReference.Source.Changed += handler;
            handler?.Invoke(this, EventArgs.Empty);
        }

        protected void BindCollection(DataSourceReference dataSourceReference, EventHandler<CollectionChangedEventArgs> handler, Action unbindCallback = null)
        {
            if (!(dataSourceReference?.Source is IDataSourceList dataSourceList)) return;

            bindings.Add(new Entry
            {
                DataSourceReference = dataSourceReference,
                CollectionHandler = handler,
                UnbindCallback = unbindCallback
            });

            dataSourceList.CollectionChanged += handler;

            var i = 0;
            foreach (var item in dataSourceList)
            {
                handler?.Invoke(dataSourceList, CollectionChangedEventArgs.ItemAdded(item, i++));
            }
        }

        protected void Unbind(DataSourceReference dataSourceReference)
        {
            var idx = bindings.FindIndex(x => x.DataSourceReference == dataSourceReference);
            if (idx == -1) return;

            Unbind(idx, true);
        }

        protected virtual void OnDisable()
        {
            if (UnbindOnDisable) UnbindAll();
        }

        protected virtual void OnDestroy()
        {
            if (UnbindOnDestroy) UnbindAll();
        }

        private void Unbind(int bindingEntryIdx, bool removeEntry)
        {
            var entry = bindings[bindingEntryIdx];
            if (entry.DataSourceReference.Source is IDataSourceList dataSourceList)
            {
                dataSourceList.CollectionChanged -= entry.CollectionHandler;
                foreach (var item in dataSourceList)
                {
                    entry.CollectionHandler?.Invoke(dataSourceList, CollectionChangedEventArgs.ItemRemoved(item, 0));
                }
            }
            else if (entry.DataSourceReference.Source is IDataSource dataSource)
            {
                dataSource.Changed -= entry.Handler;
            }
            entry.UnbindCallback?.Invoke();
            if (removeEntry) bindings.EraseSwap(bindingEntryIdx);
        }

        private void UnbindAll()
        {
            for (var i = bindings.Count - 1; i >= 0; --i)
            {
                Unbind(i, false);
            }
            bindings.Clear();
        }

#if UNITY_EDITOR
        public void LocateViewModel()
        {
            if (ViewModel.GetType() == typeof(GameObject)) {
                GameObject g = ViewModel as GameObject;
                ViewModel = g.GetComponent<ViewModel>();
                Rebind();
            }
        }

        public void Rebind()
        {
            UnbindAll();
            Setup();
        }
#endif
    }
}
