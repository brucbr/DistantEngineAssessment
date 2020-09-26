using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DistantEngine.Graphics;
using DistantEngine.Objects.Components;

namespace DistantEngine.Objects.TileMapping
{
    public class Tile
    {
        public Vector2D Position { get; set; } = new Vector2D();
        private Dictionary<Type, SortedList<int, ITileComponent>> _table = new Dictionary<Type, SortedList<int, ITileComponent>>();
        public Dictionary<Type, SortedList<int, ITileComponent>> Table
        {
            get => _table;
            set => _table = value;
        }

        /// <summary>
        /// Constructor for TileMap. Allows for XML storage.
        /// </summary>
        public Tile()
        {
        }

        public virtual void Initialise()
        {
            
        }

        public virtual void Update()
        {
            UpdateComponents();
        }

        public virtual void Draw()
        {
            RenderComponents();
        }
        
        
        #region Component Methods
        /// <summary>
        /// Update all components belonging to object
        /// </summary>
        public void UpdateComponents()
        {
            foreach (var (key, value) in _table)
            {
                foreach (var (id, component) in value)
                {
                    try
                    {
                        component.Update();
                    }
                    catch (NullReferenceException e)
                    {
                        
                    }
                }
            }
        }
        

        /// <summary>
        ///    Cause game object to render in next render update.
        /// </summary
        public void RenderComponents()
        {
            foreach (var (key, value) in _table)
            {
                foreach (var (id, component) in value)
                {
                    try
                    {
                        component.Draw();
                    }
                    catch (NullReferenceException e)
                    {
                        
                    }
                }
            }
        }
        #endregion
        
        #region Component Management
        /// <summary>
        /// Create component of type 'T' with parameters 'args'
        /// </summary>
        /// <param name="args">Arguments to pass through to component</param>
        /// <typeparam name="T">Type of component</typeparam>
        public void AddComponent<T>()
        {
            var component = (ITileComponent) Activator.CreateInstance(typeof(T), this);
            Debug.Assert(component != null, nameof(component) + " != null");
            int maxVal;
            try
            {
                SortedList<int, ITileComponent> sSet;
                if (_table.TryGetValue(component.GetType(), out sSet))
                {
                    maxVal = sSet.Last().Key;
                }
                else
                {
                    _table[component.GetType()] = new SortedList<int, ITileComponent>();
                    maxVal = 0;
                }
            }
            catch (InvalidOperationException e)
            {
                // This should only happen when the list has no entries.
                maxVal = 0;
            }
            var newValue = maxVal + 1;
            _table[component.GetType()].Add(newValue, component);
        }

        
        /// <summary>
        /// Fetch component of first and type 'T'
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <returns>Component</returns>
        public T GetComponent<T>()
        {
            var found = false;
            ITileComponent returnValue = null;
            if (!_table.TryGetValue(typeof(T), out var sSet)) return default;
            for (var i = 0; found == false; i++)
            {
                sSet.TryGetValue(i, out returnValue);
                if (returnValue != null)
                {
                    found = true;
                }
            }
            return (T) returnValue;
        }

        /// <summary>
        /// Fetch component of defined ID and type 'T'
        /// </summary>
        /// <param name="id">ID of component</param>
        /// <typeparam name="T">Type of component</typeparam>
        /// <returns>Component</returns>
        public T GetComponent<T>(int id)
        {
            if (_table.TryGetValue(typeof(T), out SortedList<int, ITileComponent> sSet))
            {
                return (T)sSet.GetValueOrDefault(id);

            }

            return default;
        }

        /// <summary>
        /// Fetch all components of type 'T'
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <returns></returns>
        public SortedList<int, ITileComponent> GetAllComponents<T>()
        {
            SortedList<int, ITileComponent> list;
            try
            {
                _table.TryGetValue(typeof(T), out list);
            }
            catch (ArgumentNullException e)
            {
                Debug.Write("Warning: No components of type" + typeof(T).Name);
                list = null;
            }

            return list;
        }
        
        /// <summary>
        /// Set value to null, however keep the reference to a null object
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        public void RemoveComponent<T>()
        {
            if (!_table.TryGetValue(typeof(T), out SortedList<int, ITileComponent> sSet)) return;
            try
            {
                var component = sSet.GetValueOrDefault(1);
                if (component != null)
                {
                    sSet[1] = null;
                }
                throw new ArgumentNullException();
            }
            catch (ArgumentNullException e)
            {
                Debug.Write("Warning: Component of type" + typeof(T).Name + " and ID " + 1 + " does not exist (Possibly already removed)");
            }
        }

        /// <summary>
        /// Remove component of defined ID and type 'T'
        /// </summary>
        /// <param name="id">ID of component</param>
        /// <typeparam name="T">Type of component</typeparam>
        public void RemoveComponent<T>(int id)
        {
            if (!_table.TryGetValue(typeof(T), out var sSet)) return;
            try
            {
                var component = sSet.GetValueOrDefault(id);
                if (component != null)
                {
                    sSet[id] = null;
                }
                throw new ArgumentNullException();
            }
            catch (ArgumentNullException e)
            {
                Debug.Write("Warning: Component of type" + typeof(T).Name + " and ID " + 1 + " does not exist (Possibly already removed)");
            }
        }

        /// <summary>
        /// List all components for this object regardless of whether they still exist.
        /// Kind of a debug feature.
        /// </summary>
        public void PrintComponents()
        {
            foreach (var (key, value) in _table)
            {
                foreach (var (id, component) in value)
                {
                    string name;
                    try
                    {
                        name = component.GetType().Name;
                    }
                    catch (NullReferenceException e)
                    {
                        name = key.Name + " (null)";
                    }
                    Console.WriteLine(name + ": " + id);
                }
            }
        }
        
        #region ID management
        // I used a single list previously of type KeyValuePair. Richard(alittleteap0t), let me know that this would be inefficient as it must
        // search for the ID linearly. Richard then gave me some code which would save both time and resources. Essentially creating a bucket list (List of buckets).
        // Thanks Old Man Richard! (He gave his complete approval to be called this)
        // While unused, it exist as a reference to a starting point and the later merging into existing code.
        
        /// <summary>
        /// Generate new ID for component
        /// </summary>
        /// <param name="component">Component</param>
        public void GenerateId(ITileComponent component)
        {
            int maxVal;
            try
            {
                if (_table.TryGetValue(component.GetType(), out var sSet))
                {
                    maxVal = sSet.Last().Key;
                }
                else
                {
                    _table[component.GetType()] = new SortedList<int, ITileComponent>();
                    maxVal = 0;
                }
            }
            catch (InvalidOperationException e)
            {
                // This should only happen when the list has no entries.
                maxVal = 0;
            }
            var newValue = maxVal + 1;
            _table[component.GetType()].Add(newValue, component);
        }

        /// <summary>
        /// Fetch ID for component
        /// </summary>
        /// <param name="component">Component</param>
        /// <returns>ID</returns>
        private int GetId(ITileComponent component)
        {
            return _table[component.GetType()].IndexOfValue(component);
        }
        #endregion
        #endregion
    }
}