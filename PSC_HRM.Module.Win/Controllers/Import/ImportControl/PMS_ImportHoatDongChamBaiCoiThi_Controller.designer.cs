﻿namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportHoatDongChamBaiCoiThi_Controller
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
            this.btImportChamBaiCoiThi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportHDKhac
            // 
            this.btImportChamBaiCoiThi.Caption = "Import chấm bài coi thi";
            this.btImportChamBaiCoiThi.ConfirmationMessage = null;
            this.btImportChamBaiCoiThi.Id = "PMS_ImportHoatDongChamBaiCoiThi_Controller";
            this.btImportChamBaiCoiThi.ImageName = "Action_Import";
            this.btImportChamBaiCoiThi.ToolTip = "Import chấm bài coi thi từ file excel";
            this.btImportChamBaiCoiThi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportChamBaiCoiThi_Execute);
            this.Activated += PMS_ImportHoatDongChamBaiCoiThi_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportChamBaiCoiThi;
    }
}
