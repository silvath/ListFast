using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace silvath.Collections
{
    public class ListFast<T> where T : IComparable<T>
    {
        #region Attributes
            private List<T> _items = new List<T>();
            private bool _isSorted = true;
            private bool _isAddSorted = false;
            private bool _unique = false;
        #endregion
        #region Properties
            public int Count 
            {
                get 
                {
                    return (this._items.Count);
                }
            }

            public T this[int index]
            {
                get
                {
                    return (this.Get(index));
                }
            }

            public bool IsAddSorted 
            {
                set 
                {
                    if ((value) && (!this._isAddSorted))
                        this.Sort();
                    this._isAddSorted = value;
                }
                get 
                {
                    return (this._isAddSorted);
                } 
            }
            public int MinSizeSearchSeek { set; get; }
            public int MinSizeSearchStartSeek { set; get; }
            public int StepSizeSearchStartSeek { set; get; }

            public bool Unique 
            {
                set 
                {
                    this._unique = value;
                }
                get 
                {
                    return (this._unique);
                }
            }
        #endregion
        #region Constructors
            public ListFast() 
            {
                this.MinSizeSearchSeek = 16;
                this.MinSizeSearchStartSeek = 32;
                this.StepSizeSearchStartSeek = 32;
                this.IsAddSorted = true;
            }

            public ListFast(ListFast<T> list)
            {
                this.MinSizeSearchSeek = list.MinSizeSearchSeek;
                this.MinSizeSearchStartSeek = list.MinSizeSearchStartSeek;
                this.StepSizeSearchStartSeek = list.StepSizeSearchStartSeek;
                this.IsAddSorted = list.IsAddSorted;
                this.Unique = list.Unique;
                this._items.AddRange(list._items);
            }
        #endregion

        #region Add
            public void Add(T item)
            {
                AddInternal(item, this._unique);
            }

            public void AddUnique(T item) 
            {
                AddInternal(item, true);
            }

            private void AddInternal(T item, bool unique)
            {
                if (this.IsAddSorted)
                    AddInternalSorted(item, unique);
                else
                    AddInternalSequencial(item, unique);
            }

            private void AddInternalSequencial(T item, bool unique)
            {
                if ((unique) && (this.Contains(item)))
                    return;
                this._items.Add(item);
                this._isSorted = false;
            }

            private void AddInternalSorted(T item, bool unique) 
            {
                int diff;
                int? index = this.FindSeekIndex(item, out diff);
                if ((unique) && (index.HasValue) && (diff == 0))
                    return;
                if (index.HasValue)
                    this._items.Insert(index.Value, item);
                else
                    this._items.Add(item);
            }

            private int? FindSeekIndex(T item, out int diff) 
            {
                for(int i = FindSeekStart(item); i < this._items.Count;i++)
                {
                    diff = Compare(this._items[i], item);
                    if (diff >= 0)
                        return(i);
                }
                diff = 0;
                return (null);
            }
        #endregion
        #region Contains
            public bool Contains(T item) 
            {
                return (this.Find(item).HasValue);
            }
        #endregion
        #region Get
            public T Get(int index) 
            {
                return (this._items[index]);
            }
        #endregion
        #region Remove
            public void RemoveAt(int index)
            {
                this._items.RemoveAt(index);
            }

            public void Remove(T item) 
            {
                int? index = Find(item);
                if (!index.HasValue)
                    return;
                RemoveAt(index.Value);
            }
        #endregion
        #region Replace
            public void Replace(ListFast<T> listFast) 
            {
                this._items = listFast._items;
                if ((this._isSorted) && (!listFast._isSorted))
                    this._isSorted = false;
                this.EnsureSort();
            }
        #endregion
        #region Find
            private int? Find(T item) 
            {
                if ((this.IsAddSorted) || ((this._isSorted)))
                {
                    this.EnsureSort();
                    if (this._isSorted)
                        return (this.FindSeek(item));
                }
                return (this.FindScan(item));
            }

            private int? FindScan(T item)
            {
                for (int i = 0; i < this._items.Count; i++)
                {
                    int diff = Compare(item, this._items[i]);
                    if (diff == 0)
                        return (i);
                }
                return (null);
            }

            private int? FindSeek(T item)
            {
                for (int i = FindSeekStart(item); i < this._items.Count; i++)
                {
                    int diff = Compare(this._items[i], item);
                    if (diff == 0)
                        return (i);
                    if (diff > 0)
                        return (null);
                }
                return (null);
            }

            private int FindSeekStart(T item)
            {
                int count = this._items.Count;
                if (count <= this.MinSizeSearchStartSeek)
                    return (0);
                int end = count - 1;
                //Start
                if (Compare(this._items[0], item) > 0)
                    return (0);
                //End
                if (Compare(this._items[end], item) < 0)
                    return (count);
                //Middle
                int endPair = (end - (end % 2 > 0 ? 1 : 0));
                if (Compare(this._items[endPair], item) < 0)
                    return (endPair);
                return (FindSeekStart(item, 0, endPair / 2, endPair));
            }

            private int FindSeekStart(T item, int start, int middle, int end)
            {
                if ((end - start) < StepSizeSearchStartSeek)
                    return (start);
                int diff = Compare(this._items[middle], item);
                if (diff == 0)
                    return (middle);
                else if (diff < 0)
                    return (FindSeekStart(item, middle, middle + ((end - middle) / 2), end));
                else
                    return (FindSeekStart(item, start, middle - ((middle - start) / 2), middle));
            } 

            private int Compare(T value1, T value2)
            {
                return (value1.CompareTo(value2));
            }
        #endregion

        #region Sort
            private void EnsureSort()
            {
                if (this._isSorted)
                    return;
                if (this._items.Count < this.MinSizeSearchSeek)
                    return;
                this.Sort();
            }

            private void Sort()
            {
                if (this._isSorted)
                    return;
                for (int i = 1; i < this._items.Count; i++)
                {
                    T itemBefore = this._items[i - 1];
                    T itemCurrent = this._items[i];
                    int diff = Compare(itemBefore, itemCurrent);
                    if (diff <= 0)
                        continue;
                    this._items[i] = itemBefore;
                    this._items[i - 1] = itemCurrent;
                    i = i - 2;
                    if (i < 0)
                        i = 0;
                }
                this._isSorted = true;
            }
        #endregion

        #region Equal
            public bool IsEqual(ListFast<T> list) 
            {
                if (this.Count != list.Count)
                    return (false);
                //Seek
                if ((this._isSorted) && (list._isSorted))
                    return (IsEqualSeek(list));
                //Scan
                return (IsEqualScan(list)); 
            }

            private bool IsEqualSeek(ListFast<T> list) 
            {
                for (int i = 0; i < this.Count; i++)
                    if (Compare(this[i], list[i]) != 0)
                        return (false);
                return (true);
            }

            private bool IsEqualScan(ListFast<T> list) 
            {
                for (int i = 0; i < this.Count; i++)
                    if (!list.Contains(this[i]))
                    return (false);
                return (true);
            } 
        #endregion
        #region Intersection
            public ListFast<int> Intersection(ListFast<int> list1, ListFast<int> list2)
            {
                ListFast<int> list = new ListFast<int>();
                for (int i = list1.Count; i >= 0; i--)
                {
                    int value = list1[i];
                    if (list2.Contains(value))
                        list.Add(value);
                }
                return (list);
            }
        #endregion
        #region Difference
            public bool Difference(ListFast<T> listFastReference, out ListFast<T> listFastBaseDifference, out ListFast<T> listFastReferenceDifference) 
            {
                //Seek
                //if ((this._isSorted) && (listFastReference._isSorted))
                //    this.DifferenceSeek(listFastReference, out listFastBaseDifference, out listFastReferenceDifference);
                //else //Scan
                    this.DifferenceScan(listFastReference, out listFastBaseDifference, out listFastReferenceDifference);
                return ((listFastBaseDifference.Count > 0) || (listFastReferenceDifference.Count > 0));
            }

            private void DifferenceSeek(ListFast<T> listReference, out ListFast<T> listFastBaseDifference, out ListFast<T> listFastReferenceDifference)
            {
                //TODO: Work over here
                listFastBaseDifference = new ListFast<T>() { Unique = true };
                listFastReferenceDifference = new ListFast<T>() { Unique = true };
                if (this.Count < listReference.Count)
                {
                    int count = this.Count;
                    for (int i = 0; i < count; i++)
                    {

                    }
                }else{

                }
            }

            private void DifferenceScan(ListFast<T> listFastReference, out ListFast<T> listFastBaseDifference, out ListFast<T> listFastReferenceDifference)
            {
                listFastBaseDifference = new ListFast<T>() { Unique = true };
                listFastReferenceDifference = new ListFast<T>() { Unique = true };
                int count = this.Count;
                //Base to Reference
                for (int i = 0; i < count; i++) 
                {
                    T value = this._items[i];
                    if (!listFastReference.Contains(value))
                        listFastBaseDifference.Add(value);
                }
                //Reference to Base
                count = listFastReference.Count;
                for (int i = 0; i < count; i++)
                {
                    T value = listFastReference._items[i];
                    if (!listFastReferenceDifference.Contains(value))
                        listFastReferenceDifference.Add(value);
                }
            } 
        #endregion
    }
}
