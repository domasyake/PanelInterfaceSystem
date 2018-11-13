namespace PanelInterfaceSystem{
	/// <summary>
	/// This Interface indicates that it is an controll target.
	/// Panel must inherit ISelectableIPanel.
	/// </summary>
	public interface ISelectablePanel{
		bool IsActive{ get; set; }
		void OnSelect();
		void RemoveSelect();
		void Submit();
	}
}