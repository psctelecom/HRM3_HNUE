﻿namespace PSC_HRM.Module.Win.Controllers.PMS.ThoiKhoaBieu
{
    partial class ThoiKhoaBieu_QuyDoi_Controller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btQuyDoi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btQuyDoi
            // 
            this.btQuyDoi.Caption = "Quy đổi";
            this.btQuyDoi.ConfirmationMessage = null;
            this.btQuyDoi.Id = "btQuyDoi";
            this.btQuyDoi.ImageName = "BO_QuyDoi";
            this.btQuyDoi.ToolTip = "Quy đổi khối lượng giảng dạy thành giờ thanh toán thù lao";
            this.btQuyDoi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btQuyDoi_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btQuyDoi;
    }
}
