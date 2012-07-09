using System;
using System.Text;

namespace XPTable.Sorting
{
	/// <summary>
	/// Defimes the type of sort to be used
	/// </summary>
	public enum SortType
	{
		/// <summary>
		/// System to determine Sort method
		/// </summary>
		AutoSort,
		/// <summary>
		/// Use Heap Sort method
		/// </summary>
		HeapSort,
		/// <summary>
		/// Use Insertion Sort Method
		/// </summary>
		InsertionSort,
		/// <summary>
		/// Use Merge Sort Method
		/// </summary>
		MergeSort,
		/// <summary>
		/// Use Shell Sort Method
		/// </summary>
		ShellSort,
	}
}
