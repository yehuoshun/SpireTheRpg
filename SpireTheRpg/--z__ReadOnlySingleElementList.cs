using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200007D RID: 125
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
	// Token: 0x06000579 RID: 1401 RVA: 0x0001DB02 File Offset: 0x0001BD02
	public <>z__ReadOnlySingleElementList(T item)
	{
		this._item = item;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0001DB11 File Offset: 0x0001BD11
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001DB1E File Offset: 0x0001BD1E
	int ICollection.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001DB21 File Offset: 0x0001BD21
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001DB24 File Offset: 0x0001BD24
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0001DB27 File Offset: 0x0001BD27
	void ICollection.CopyTo([Nullable(1)] Array array, int index)
	{
		array.SetValue(this._item, index);
	}

	// Token: 0x1700016D RID: 365
	object IList.this[int index]
	{
		[return: Nullable(2)]
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001DB58 File Offset: 0x0001BD58
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001DB5B File Offset: 0x0001BD5B
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0001DB5E File Offset: 0x0001BD5E
	int IList.Add([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0001DB65 File Offset: 0x0001BD65
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0001DB6C File Offset: 0x0001BD6C
	bool IList.Contains([Nullable(2)] object value)
	{
		return EqualityComparer<T>.Default.Equals(this._item, (T)((object)value));
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0001DB84 File Offset: 0x0001BD84
	int IList.IndexOf([Nullable(2)] object value)
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object)value)))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0001DBA1 File Offset: 0x0001BDA1
	void IList.Insert(int index, [Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
	void IList.Remove([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001DBAF File Offset: 0x0001BDAF
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001DBB6 File Offset: 0x0001BDB6
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001DBC3 File Offset: 0x0001BDC3
	int IReadOnlyCollection<T>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000171 RID: 369
	T IReadOnlyList<T>.this[int index]
	{
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
	int ICollection<T>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001DBDA File Offset: 0x0001BDDA
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0001DBDD File Offset: 0x0001BDDD
	void ICollection<T>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
	void ICollection<T>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001DBEB File Offset: 0x0001BDEB
	bool ICollection<T>.Contains(T item)
	{
		return EqualityComparer<T>.Default.Equals(this._item, item);
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001DBFE File Offset: 0x0001BDFE
	void ICollection<T>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] array, int arrayIndex)
	{
		array[arrayIndex] = this._item;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001DC0D File Offset: 0x0001BE0D
	bool ICollection<T>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000174 RID: 372
	T IList<T>.this[int index]
	{
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0001DC2C File Offset: 0x0001BE2C
	int IList<T>.IndexOf(T item)
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, item))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001DC44 File Offset: 0x0001BE44
	void IList<T>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001DC4B File Offset: 0x0001BE4B
	void IList<T>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x040001BC RID: 444
	[CompilerGenerated]
	private readonly T _item;

	// Token: 0x02000153 RID: 339
	private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0002BB9E File Offset: 0x00029D9E
		public Enumerator(T item)
		{
			this.System.Collections.Generic.IEnumerator<T>.Current = item;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002BBAD File Offset: 0x00029DAD
		object IEnumerator.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0002BBBA File Offset: 0x00029DBA
		T IEnumerator<T>.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		bool IEnumerator.MoveNext()
		{
			return !this._moveNextCalled && (this._moveNextCalled = true);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002BBE5 File Offset: 0x00029DE5
		void IEnumerator.Reset()
		{
			this._moveNextCalled = false;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002BBEE File Offset: 0x00029DEE
		void IDisposable.Dispose()
		{
		}

		// Token: 0x040004E1 RID: 1249
		[CompilerGenerated]
		private readonly T _item;

		// Token: 0x040004E2 RID: 1250
		[CompilerGenerated]
		private bool _moveNextCalled;
	}
}
