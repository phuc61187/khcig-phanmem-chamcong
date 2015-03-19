﻿/*
 * Created by SharpDevelop.
 * User: mjackson
 * Date: 05/03/2010
 * Time: 09:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace CapNhatGioChamCong
{
	/// <summary>
	/// The dialogue displayed by a WaitWindow instance.
	/// </summary>
	internal partial class WaitWindowGUI : Form
	{
		public WaitWindowGUI(WaitWindow parent){
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this._Parent = parent;
			
			//	Position the window in the top right of the main screen.
			this.Top = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Bottom - this.Height) / 2d);// + 32;
			this.Left = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Right - this.Width) / 2d);// - 32;
		}

		private WaitWindow _Parent;
		private delegate T FunctionInvoker<T>();
    	internal object _Result;
    	internal Exception _Error;
    	private IAsyncResult threadResult;
    	
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
			base.OnPaint(e);
			//	Paint a 3D border
			ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Raised);
		}

		protected override void OnShown(EventArgs e){
			base.OnShown(e);
						
			//   Create Delegate
			FunctionInvoker<object> threadController = new FunctionInvoker<object>(this.DoWork);

			//   Execute on secondary thread.
			this.threadResult = threadController.BeginInvoke(this.WorkComplete, threadController);
    	}
		
		internal object DoWork(){
			//	Invoke the worker method and return any results.
			WaitWindowEventArgs e = new WaitWindowEventArgs(this._Parent, this._Parent._Args);
			if ((this._Parent._WorkerMethod != null)){
				this._Parent._WorkerMethod(this, e);
			}
			return e.Result;
		}

    	private void WorkComplete(IAsyncResult results){
    		if (!this.IsDisposed){
	    		if (this.InvokeRequired){
	    			this.Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(this.WorkComplete), results);
	    		} else {
	    			//	Capture the result
	    			try {
						this._Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
	    			} catch (Exception ex) {
	    				//	Grab the Exception for rethrowing after the WaitWindow has closed.
						this._Error = ex;
	    			}
					this.Close();
	    		}
    		}
    	}
		
		internal void SetMessage(string message){
			this.MessageLabel.Text = message;
		}
    	
    	internal void Cancel(){
    		this.Invoke(new MethodInvoker(this.Close), null);
    	}
	}
}