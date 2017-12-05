using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

namespace asgn5v1
{
	/// <summary>
	/// Summary description for Transformer.
	/// </summary>
	public class Transformer : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		//private bool GetNewData();

		// basic data for Transformer

		int numpts = 0;
		int numlines = 0;
		bool gooddata = false;		
		double[,] vertices;
		double[,] scrnpts;
        double[,] initialImage;
		double[,] ctrans = new double[4,4];  //your main transformation matrix
        double shapeheight = 0.0d; //height of the imported shape
        double shapewidth = 0.0d; //width of the imported shape
        double shapedepth = 0.0d; //depth of the imported shape
        double minx = 0.0d; //coordinate with the smallest x value
        double miny = 0.0d; //coordinate with the smallest y value
        double minz = 0.0d; //coordinate with the smallest z value
        double[] shapecentre = new double[3]; //centre point of the imported shape
        int[,] lines;
        Timer timerX, timerY, timerZ;

        private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
		private System.Windows.Forms.ToolBarButton exitbtn;

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (200830) (H. Sharma, T. Stickney)";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("E&xit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});

            initializeTimer();
        }

        void initializeTimer()
        {
            timerX = new Timer();
            timerX.Interval = 50; // msec
            timerX.Tick += new EventHandler(timer_tickX);

            timerY = new Timer();
            timerY.Interval = 50; // msec
            timerY.Tick += new EventHandler(timer_tickY);

            timerZ = new Timer();
            timerZ.Interval = 50; // 
            timerZ.Tick += new EventHandler(timer_tickZ);
        }


        void timer_tickX(object sender, EventArgs e)
        {
            rotateX(ctrans);
            Refresh();
        }

        void timer_tickY(object sender, EventArgs e)
        {
            rotateY(ctrans);
            Refresh();
        }

        void timer_tickZ(object sender, EventArgs e)
        {
            rotateZ(ctrans);
            Refresh();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
			double temp;
			int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }
                
                GetShapeDimensions();
                GetShapeCentre();
            } // end of gooddata block	
		} // end of OnPaint

		void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
            SetInitialTransformation(ctrans);			
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
			Close();
		}

		void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
            Invalidate();
            for (int i = 0; i < scrnpts.GetLength(0); i++)
            {
                for (int j = 0; j < scrnpts.GetLength(1); j++)
                {
                    scrnpts[i, j] = initialImage[i, j];
                }
            }
            //set the transformation matrix
            setIdentity(ctrans, 4, 4);

            // Do initial Tansformations again
            SetInitialTransformation(ctrans);
        } // end of RestoreInitialImage

		bool GetNewData()
		{
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
            }
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}
			scrnpts = new double[numpts,4];
            initialImage = new double[numpts,4];
			setIdentity(ctrans,4,4);  //initialize transformation matrix to identity
			return true;
		} // end of GetNewData

		void DecodeCoords(ArrayList coorddata)
		{
			//this may allocate slightly more rows that necessary
			vertices = new double[coorddata.Count,4];
			numpts = 0;
			string [] text = null;
			for (int i = 0; i < coorddata.Count; i++)
			{
				text = coorddata[i].ToString().Split(' ',',');
				vertices[numpts,0]=double.Parse(text[0]);
				if (vertices[numpts,0] < 0.0d) break;
				vertices[numpts,1]=double.Parse(text[1]);
				vertices[numpts,2]=double.Parse(text[2]);
				vertices[numpts,3] = 1.0d;
				numpts++;						
			}
			
		}// end of DecodeCoords

		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]=int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

		void setIdentity(double[,] A,int nrow,int ncol)
		{
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    A[i, j] = 0.0d;
                }
                A[i, i] = 1.0d;
            }
        }// end of setIdentity

        private void Transformer_Load(object sender, System.EventArgs e)
		{
			
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == transleftbtn)
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                translateLeft(ctrans);
                Refresh();
			}
			if (e.Button == transrightbtn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                translateRight(ctrans);
                Refresh();
			}
			if (e.Button == transupbtn)
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                translateUp(ctrans);
				Refresh();
			}
			
			if(e.Button == transdownbtn)
            {
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                translateDown(ctrans);
                Refresh();
			}
			if (e.Button == scaleupbtn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                scaleUp(ctrans);
                Refresh();
			}
			if (e.Button == scaledownbtn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                scaleDown(ctrans);
                Refresh();
			}
			if (e.Button == rotxby1btn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                rotateX(ctrans);
                Refresh();
            }
			if (e.Button == rotyby1btn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                rotateY(ctrans);
                Refresh();
            }
			if (e.Button == rotzby1btn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                rotateZ(ctrans);
                Refresh();
            }

			if (e.Button == rotxbtn) 
			{
                if (timerZ.Enabled || timerX.Enabled || timerY.Enabled)
                {
                    timerX.Stop();
                    timerY.Stop();
                    timerZ.Stop();
                }
                else
                {
                    timerX.Start();
                }
            }

            if (e.Button == rotybtn) 
			{
                if (timerZ.Enabled || timerX.Enabled || timerY.Enabled)
                {
                    timerX.Stop();
                    timerY.Stop();
                    timerZ.Stop();
                }
                else
                {
                    timerY.Start();
                }
            }
			
			if (e.Button == rotzbtn) 
			{
                if (timerZ.Enabled || timerX.Enabled || timerY.Enabled)
                {
                    timerX.Stop();
                    timerY.Stop();
                    timerZ.Stop();
                }
                else
                {
                    timerZ.Start();
                }
            }

			if(e.Button == shearleftbtn)
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                shearLeft(ctrans);
                Refresh();
			}

			if (e.Button == shearrightbtn) 
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                shearRight(ctrans);
                Refresh();
			}

			if (e.Button == resetbtn)
			{
                timerX.Stop();
                timerY.Stop();
                timerZ.Stop();
                RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
				Close();
			}

		}

        //Get the width, height, and depth of the shapes, as well as
        //the coordinates with the minimum x, y, and z values
        private void GetShapeDimensions()
        {
            double max = scrnpts[0, 0];
            double min = scrnpts[0, 0];

            //Get width of shape
            for (int i = 0; i < numpts - 1; i++)
            {
                //Find the maximum x value
                if (max < scrnpts[i + 1, 0])
                {
                    max = scrnpts[i + 1, 0];
                }

                //Find the minimum x value and save the coordinate point
                if (min > scrnpts[i + 1, 0])
                {
                    min = scrnpts[i + 1, 0];
                    //Save the x value of the coordinate point
                    minx = scrnpts[i + 1, 0];
                }
            }
            shapewidth = max - min;

            max = scrnpts[0, 1];
            min = scrnpts[0, 1];

            //Get height of shape
            for (int i = 0; i < numpts - 1; i++)
            {
                //Find the maximum y value
                if (max < scrnpts[i + 1, 1])
                {
                    max = scrnpts[i + 1, 1];
                }

                if (min > scrnpts[i + 1, 1])
                {
                    min = scrnpts[i + 1, 1];
                    //Save the y value of the coordinate point
                    miny = scrnpts[i + 1, 1];
                }
            }
            shapeheight = max - min;

            max = scrnpts[0, 2];
            min = scrnpts[0, 2];

            //Get depth of shape
            for (int i = 0; i < numpts - 1; i++)
            {
                //Find the maximum z value
                if (max < scrnpts[i + 1, 2])
                {
                    max = scrnpts[i + 1, 2];
                }

                if (min > scrnpts[i + 1, 2])
                {
                    min = scrnpts[i + 1, 2];
                    //Save the z value of the coordinate point
                    minz = scrnpts[i + 1, 2];
                }
            }
            shapedepth = max - min;
        }

        //Get the centre point of the shape
        private void GetShapeCentre()
        {
            shapecentre[0] = minx + shapewidth / 2;
            shapecentre[1] = miny + shapeheight / 2;
            shapecentre[2] = minz + shapedepth / 2;
        }

        private void SetInitialTransformation(double[,] A)
        {
            Invalidate();
            double[] screencentre = new double[2];

            //Get the centre coordinate for the form
            screencentre[0] = this.Width / 2;
            screencentre[1] = this.Height / 2;

            // Translating -10 left and -10 up
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -10, -10, 0, 1 } };
            matMult(A, B);

            // Reflecting in x-axis
            double[,] C = new double[,] { { 1, 0, 0, 0 }, { 0, -1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            matMult(A, C);

            // Get the factor by which to scale the shape
            double scalefactor = this.Height / 2 / 21;
            double[,] D = new double[,] { { scalefactor, 0, 0, 0 }, { 0, scalefactor, 0, 0 }, { 0, 0, scalefactor, 0 }, { 0, 0, 0, 1 } };
            matMult(A, D);

            // Translating Width/2 right and Height/2 Down
            double[,] E = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, {screencentre[0], screencentre[1], 0, 1 } };
            matMult(A, E);

            for (int i = 0; i < scrnpts.GetLength(0); i++)
            {
                for (int j = 0; j < scrnpts.GetLength(1); j++)
                {
                    initialImage[i, j] = scrnpts[i, j];
                }
            }

        }

        // Implementing Matrix Multiplication
        private void matMult(double[,] A, double[,] B)
        {
            // To hold the product of multiplication temporarily
            double[,] temp = new double[4, 4];

            // GetLength(i) returns the number of elements in the ith dimension.
            if (A.GetLength(1) == 4 && B.GetLength(0) == 4) {
                for (int outer = 0; outer < A.GetLength(0); outer++)
                {
                    for (int inner = 0; inner < B.GetLength(1); inner++)
                    {
                        for (int ab = 0; ab < B.GetLength(1); ab++)
                        {
                            temp[outer,inner] += A[outer,ab] * B[ab,inner];
                        }
                    }
                }
            }

            // Putting the multiplication result back in A
            for (int r = 0; r< 4; r++)
            {
                for (int c= 0; c< 4;c++)
                {
                    A[r, c] = temp[r, c];
                }
            }

        }

        // Scaling up by 10%
        private void scaleUp(double[,] A)
        {
            double transX = shapecentre[0];
            double transY = shapecentre[1];
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } }; 

            // Get the factor by which to scale the shape
            double scalefactor = 1.1;
            double[,] C = new double[,] { { scalefactor, 0, 0, 0 }, { 0, scalefactor, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Scaling down by 10%
        private void scaleDown(double[,] A)
        {
            double transX = shapecentre[0];
            double transY = shapecentre[1];
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } };

            // Get the factor by which to scale the shape
            double scalefactor = 1.1;
            double[,] C = new double[,] { { 1/scalefactor, 0, 0, 0 }, { 0, 1/scalefactor, 0, 0 }, { 0, 0, 1/scalefactor, 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Translating left by 75 pixels on each click
        private void translateLeft(double[,] A)
        {
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -75, 0, 0, 1 } };
            matMult(A, B);
        }

        // Translating right by 75 pixels on each click
        private void translateRight(double[,] A)
        {
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 75, 0, 0, 1 } };
            matMult(A, B);
        }

        // Translating down by 35 pixels on each click
        private void translateDown(double[,] A)
        {
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 35, 0, 1 } };
            matMult(A, B);
        }

        // Translating up by 35 pixels on each click
        private void translateUp(double[,] A)
        {
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, -35, 0, 1 } };
            matMult(A, B);
        }

        // Rotating around x-axis by 0.05 radians on each click
        private void rotateX(double[,] A)
        {
            double transX = shapecentre[0];
            double transY = shapecentre[1];
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } };

            // Rotating around x-axis
            double[,] C = new double[,] { { 1, 0, 0, 0 }, { 0, Math.Cos(0.05), -Math.Sin(0.05), 0 }, { 0, Math.Sin(0.05), Math.Cos(0.05), 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Rotating around y-axis by 0.05 radians on each click
        private void rotateY(double[,] A)
        {
            double transX = shapecentre[0];
            double transY = shapecentre[1];
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } };

            // Rotating around y-axis
            double[,] C = new double[,] { { Math.Cos(0.05), 0, -Math.Sin(0.05), 0 }, { 0, 1, 0, 0 }, { Math.Sin(0.05), 0, Math.Cos(0.05), 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Rotating around z-axis by 0.05 radians on each click
        private void rotateZ(double[,] A)
        {
            double transX = shapecentre[0];
            double transY = shapecentre[1];
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } };

            // Rotating around x-axis
            double[,] C = new double[,] { { Math.Cos(0.05), -Math.Sin(0.05), 0, 0 }, { Math.Sin(0.05), Math.Cos(0.05), 0, 0 }, { 0,0, 1, 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Shearing left
        private void shearLeft(double[,] A)
        {
            double topHeight = shapecentre[1] - miny; // Height of top half of the image
            double transX = minx;
            double transY = miny + 2*topHeight;
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, {-transX, -transY, -transZ, 1 } };

            // Shearing in the left direction
            double[,] C = new double[,] { {1, 0, 0, 0 }, { 0.1, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

        // Shearing Right
        private void shearRight(double[,] A)
        {
            double topHeight = shapecentre[1] - miny; // Height of top half of the image
            double transX = minx;
            double transY = miny + 2 * topHeight;
            double transZ = shapecentre[2];

            // Translating back to 0,0
            double[,] B = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -transX, -transY, -transZ, 1 } };

            // Shearing in the left direction
            double[,] C = new double[,] { { 1, 0, 0, 0 }, { -0.1, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            matMult(B, C);

            // Translating back to initial Position
            double[,] D = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { transX, transY, transZ, 1 } };
            matMult(B, D);

            matMult(A, B);
        }

    }
    
    // Reset image to its initial position
    // Continous Rotation
}
