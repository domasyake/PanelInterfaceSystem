namespace PanelInterfaceSystem{
	/// <summary>
	/// This Interface indicates that it is an controll target.
	/// Panel must inherit ISelectableIPanel.
	/// </summary>
	public interface ISelectablePanel{
		bool IsActive{ get; set; }
		
		/// <summary>
		/// Called when selected.
		/// </summary>
		void OnSelect();
		
		/// <summary>
		/// Called when it is not selected.
		/// </summary>
		void RemoveSelect();
		
		/// <summary>
		/// Called when decided.
		/// </summary>
		void Submit();
	}
}