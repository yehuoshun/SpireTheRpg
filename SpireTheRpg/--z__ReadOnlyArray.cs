using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200007C RID: 124
[CompilerGenerated]
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
	// Token: 0x06000559 RID: 1369 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
	public <>z__ReadOnlyArray(T[] items)
	{
		this._items = items;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001D9E2 File Offset: 0x0001BBE2
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001D9EF File Offset: 0x0001BBEF
	int ICollection.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001D9F9 File Offset: 0x0001BBF9
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001D9FC File Offset: 0x0001BBFC
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001D9FF File Offset: 0x0001BBFF
	void ICollection.CopyTo([Nullable(1)] Array array, int index)
	{
		this._items.CopyTo(array, index);
	}

	// Token: 0x17000162 RID: 354
	object IList.this[int index]
	{
		[return: Nullable(2)]
		get
		{
			return this._items[index];
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001DA28 File Offset: 0x0001BC28
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001DA2B File Offset: 0x0001BC2B
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001DA2E File Offset: 0x0001BC2E
	int IList.Add([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001DA35 File Offset: 0x0001BC35
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001DA3C File Offset: 0x0001BC3C
	bool IList.Contains([Nullable(2)] object value)
	{
		return this._items.Contains(value);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001DA4A File Offset: 0x0001BC4A
	int IList.IndexOf([Nullable(2)] object value)
	{
		return this._items.IndexOf(value);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001DA58 File Offset: 0x0001BC58
	void IList.Insert(int index, [Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001DA5F File Offset: 0x0001BC5F
	void IList.Remove([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001DA66 File Offset: 0x0001BC66
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001DA6D File Offset: 0x0001BC6D
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001DA7A File Offset: 0x0001BC7A
	int IReadOnlyCollection<T>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x17000166 RID: 358
	T IReadOnlyList<T>.this[int index]
	{
		get
		{
			return this._items[index];
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001DA92 File Offset: 0x0001BC92
	int ICollection<T>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001DA9C File Offset: 0x0001BC9C
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001DA9F File Offset: 0x0001BC9F
	void ICollection<T>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001DAA6 File Offset: 0x0001BCA6
	void ICollection<T>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0001DAAD File Offset: 0x0001BCAD
	bool ICollection<T>.Contains(T item)
	{
		return this._items.Contains(item);
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001DABB File Offset: 0x0001BCBB
	void ICollection<T>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] array, int arrayIndex)
	{
		this._items.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001DACA File Offset: 0x0001BCCA
	bool ICollection<T>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000169 RID: 361
	T IList<T>.this[int index]
	{
		get
		{
			return this._items[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001DAE6 File Offset: 0x0001BCE6
	int IList<T>.IndexOf(T item)
	{
		return this._items.IndexOf(item);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
	void IList<T>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001DAFB File Offset: 0x0001BCFB
	void IList<T>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x040001BB RID: 443
	[CompilerGenerated]
	private readonly T[] _items;
}
