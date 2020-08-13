using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace WindowsFormsApp3
{
	public partial class Form1 : Form
	{
		enum ImageTypes
		{
			UNKNOWN,
			CUSTOMIZE,
			NEW_ITEM			
		}

		enum NewItemDep
		{
			SEND_TO_TRANSFER,
			KEEP_ITEM,
			QUICK_SELL,
			NOT_DETECTED
		}

		enum CustomizeActivePos
		{
			NOT_DETECTED,
			SETTINGS,
			PROFILE,
			ONLINE_SETTINGS,
			CATALOGUE,
			EDIT_TEAMS,
			TEAM_SHEETS,
			CREATE_PLAYER,
			EA_TRAX			
		}

		enum Arrows
		{			
			DOWN,
			UP,
			RIGHT,
			LEFT
		}


		string[] customizeTemplatesPathes = new string[] { "CUSTOMIZE_label.png" };
		string[] newItemTemplatesPathes = new string[] { "NEW_ITEM_label1.png", "NEW_ITEM_label2.png", "NEW_ITEM_label3.png" };
		string[] custActiveTemplatesPathes = new string[]
		{
			"SETTINGS_label.png",
			"",
			"",
			"CATALOGUE_label.png",
			"",
			"TEAM_SHEETS_label.png",
			"",
			"EA_TRAX_label.png",
		};

		string[] sendToTransferTemplatePathes = new string[] { "SEND_TO_TRANSFER_label.png"};
		string[] keepItemTemplatePathes = new string[] { "KEEP_ITEMS_label.png", "KEEP_ITEMS_label2.png", "KEEP_ITEMS_label3.png", "KEEP_ITEMS_label4.png" };
		string[] quickSellTemplatePathes = new string[] { "QUICK_SELL_label.png" };

		Dictionary<ImageTypes,List<Mat>> templateList = new Dictionary<ImageTypes, List<Mat>>();
		Dictionary<CustomizeActivePos, List<Mat>> customMenuTemplateList = new Dictionary<CustomizeActivePos, List<Mat>>();
		Dictionary<NewItemDep, List<Mat>> newItemDepTemplateList = new Dictionary<NewItemDep, List<Mat>>();

		List<Mat> customizeTemplates = new List<Mat>();
		List<Mat> newItemTemplates = new List<Mat>();

		List<Mat> sendToTransferTemplates = new List<Mat>();
		List<Mat> keepItemTemplates = new List<Mat>();
		List<Mat> quickSellTemplates = new List<Mat>();

		double minVal, maxVal;
		Point minLoc;
		Point maxLoc;
		Size rectSize;
		Mat img,result;
		string resultStr;

		CustomizeActivePos resultPos;
		NewItemDep resultDep;


		private void InitializeTemplates()
		{
			foreach (string templatePath in customizeTemplatesPathes)
			{
				this.customizeTemplates.Add(new Mat(templatePath));
			}

			foreach (string templatePath in newItemTemplatesPathes)
			{
				this.newItemTemplates.Add(new Mat(templatePath));
			}

			this.templateList.Add(ImageTypes.CUSTOMIZE,customizeTemplates);
			this.templateList.Add(ImageTypes.NEW_ITEM,newItemTemplates);

			for (int i = 1; i <= custActiveTemplatesPathes.Length; i++)
			{
				string tempPath = custActiveTemplatesPathes[i-1]; 
				if(tempPath!="")
				{
					Mat temp = new Mat(tempPath);
					List<Mat> tempList = new List<Mat>();
					tempList.Add(temp);
					this.customMenuTemplateList.Add((CustomizeActivePos)i, tempList);
				}				
			}

			foreach (string templatePath in sendToTransferTemplatePathes)
			{
				this.sendToTransferTemplates.Add(new Mat(templatePath));
			}
			foreach (string templatePath in keepItemTemplatePathes)
			{
				this.keepItemTemplates.Add(new Mat(templatePath));
			}
			foreach (string templatePath in quickSellTemplatePathes)
			{
				this.quickSellTemplates.Add(new Mat(templatePath));
			}
			this.newItemDepTemplateList.Add(NewItemDep.SEND_TO_TRANSFER, sendToTransferTemplates);
			this.newItemDepTemplateList.Add(NewItemDep.KEEP_ITEM, keepItemTemplates);
			this.newItemDepTemplateList.Add(NewItemDep.QUICK_SELL, quickSellTemplates);
		}

		private void NormalizeStartData()
		{
			img = new Mat("test.png");
			result = new Mat();
			resultStr = "";
			minLoc = new Point();
			maxLoc = new Point();
			rectSize = new Size();
			numericUpDown1.Visible = false;
			labelCurPos.Visible = false;
			labelTargetPos.Visible = false;
			btnFindSteps.Visible = false;
			labelResultDep.Visible = false;
		}

		public Form1()
		{
			InitializeComponent();
			InitializeTemplates();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				NormalizeStartData();				

				ImageTypes resultType = pictureTypeDetect(img, templateList, result, TemplateMatchingType.Sqdiff);

				switch (resultType) 
				{
					case ImageTypes.CUSTOMIZE:
						CvInvoke.Rectangle(img, new Rectangle(minLoc, rectSize), new MCvScalar(255, 0, 0));
						pictureBox1.Image = img.ToBitmap();
						numericUpDown1.Visible = true;
						labelCurPos.Visible = true;
						labelTargetPos.Visible = true;
						btnFindSteps.Visible = true;
						resultStr = "Customize";
						resultPos = CustomizeActivePosDetect(img, customMenuTemplateList, result, TemplateMatchingType.Sqdiff);
						labelCurPos.Text = "Current pos in custom menu " + (int)resultPos +" "+ resultPos;
						labelTargetPos.Text = "Select Target Pos";
						switch (resultPos)
						{
							case CustomizeActivePos.SETTINGS:
								resultStr += " active pos Settings";
								break;
							case CustomizeActivePos.PROFILE:
								resultStr += " active pos PROFILE";
								break;
							case CustomizeActivePos.ONLINE_SETTINGS:
								resultStr += " active pos ONLINE_SETTINGS";
								break;
							case CustomizeActivePos.CATALOGUE:
								resultStr += " active pos CATALOGUE";
								break;
							case CustomizeActivePos.EDIT_TEAMS:
								resultStr += " active pos EDIT_TEAMS";
								break;
							case CustomizeActivePos.TEAM_SHEETS:
								resultStr += " active pos TEAM_SHEETS";
								break;
							case CustomizeActivePos.CREATE_PLAYER:
								resultStr += " active pos CREATE_PLAYER";
								break;
							case CustomizeActivePos.EA_TRAX:
								resultStr += " active pos EA_TRAX";
								break;
						}
						break;
					case ImageTypes.NEW_ITEM:
						CvInvoke.Rectangle(img, new Rectangle(minLoc, rectSize), new MCvScalar(255, 0, 0));
						pictureBox1.Image = img.ToBitmap();
						resultStr = "New Item";
						resultDep = NewItemDepDetect(img,newItemDepTemplateList,result,TemplateMatchingType.Sqdiff);
						labelResultDep.Visible = true;
						switch (resultDep)
						{
							case NewItemDep.SEND_TO_TRANSFER:
								resultStr += " active dep send to transfer";
								labelResultDep.Text = "Current section is " + NewItemDep.SEND_TO_TRANSFER + "Press-" + Arrows.DOWN + " to " + NewItemDep.KEEP_ITEM;
								break;
							case NewItemDep.KEEP_ITEM:
								resultStr += " active dep keep item";
								labelResultDep.Text = "Current section is " +  NewItemDep.KEEP_ITEM;
								break;
							case NewItemDep.QUICK_SELL:
								resultStr += " active dep quick sell";
								labelResultDep.Text = "Current section is " + NewItemDep.QUICK_SELL + "Press-" + Arrows.UP + " to " + NewItemDep.KEEP_ITEM;
								break;
						}
						break;
					case ImageTypes.UNKNOWN:
						resultStr = "Unknown";
						break;
				}					
				label1.Text = resultStr + " MinVal =" + minVal;
				

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private ImageTypes pictureTypeDetect(Mat sourceImg,Dictionary<ImageTypes,List<Mat>> templates, Mat result, TemplateMatchingType method, Mat mask = null)
		{
			foreach  (KeyValuePair<ImageTypes,List<Mat>> tempArray in templates)
			{				
				foreach ( Mat templ in tempArray.Value)
				{
					CvInvoke.MatchTemplate(sourceImg, templ, result, method, mask);
					CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
					if(minVal<3000)
					{
						rectSize = new Size(templ.Cols, templ.Rows); 
						return tempArray.Key;
					}
				}
			}
			return ImageTypes.UNKNOWN;
		}

		private NewItemDep NewItemDepDetect(Mat sourceImg, Dictionary<NewItemDep, List<Mat>> templates, Mat result, TemplateMatchingType method, Mat mask = null)
		{
			foreach (KeyValuePair<NewItemDep, List<Mat>> tempArray in templates)
			{
				foreach (Mat templ in tempArray.Value)
				{
					CvInvoke.MatchTemplate(sourceImg, templ, result, method, mask);
					CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
					if (minVal < 3000)
					{
						rectSize = new Size(templ.Cols, templ.Rows);
						return tempArray.Key;
					}
				}
			}
			return NewItemDep.NOT_DETECTED;
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Enabled = true;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDown1.Value < 1)
				numericUpDown1.Value = 1;
			if (numericUpDown1.Value > 8)
				numericUpDown1.Value = 8;
		}

		private void label2_Click_1(object sender, EventArgs e)
		{

		}

		private void labelCurPos_Click(object sender, EventArgs e)
		{

		}

		private void btnFindSteps_Click(object sender, EventArgs e)
		{
			string  resultSteps = "";
			CustomizeActivePos target = (CustomizeActivePos)numericUpDown1.Value;
			if (target == resultPos) resultSteps = "SAME TARGET!";
			else
			{
				int curRowIndex = (int)(resultPos - 1) / 4;
				int curColIndex = (int)(resultPos - 1) % 4;
				int targetRowIndex = (int)(target - 1) / 4;
				int targetColIndex = (int)(target - 1) % 4;
				int rowDiff = targetRowIndex - curRowIndex;
				int colDiff = targetColIndex - curColIndex;
				if (colDiff > 0)
				{
					for (int i = 0; i < colDiff; i++)
					{
						resultSteps += " RIGHT ->";
					}
				}
				if (colDiff < 0)
				{
					for (int i = 0; i > colDiff; i--)
					{
						resultSteps = resultSteps + " LEFT <- ";
					}
				}
				if (rowDiff > 0) resultSteps += "DOWN v";

				if (rowDiff < 0) resultSteps  += " UP ^ ";
			}

			labelStepsCur.Text = resultSteps + " => " + target;
			labelStepsCur.Visible = true;

		}

		private CustomizeActivePos CustomizeActivePosDetect(Mat sourceImg, Dictionary<CustomizeActivePos, List<Mat>> templates, Mat result, TemplateMatchingType method, Mat mask = null)
		{
			foreach (KeyValuePair<CustomizeActivePos, List<Mat>> tempArray in templates)
			{
				foreach (Mat templ in tempArray.Value)
				{
					CvInvoke.MatchTemplate(sourceImg, templ, result, method, mask);
					CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
					if (minVal < 3000)
					{
						rectSize = new Size(templ.Cols, templ.Rows);
						return tempArray.Key;
					}
				}
			}
			return CustomizeActivePos.NOT_DETECTED;
		}

		private void nextBtn_click(object sender, EventArgs e)
		{
			new WebClient().DownloadFile(new Uri("http://185.80.129.249:4222/getImage"), "new.png");
			//string path = System.IO.Path.GetFullPath("test.png");
			//openFileDialog1.ShowDialog();
			//string pathImg = openFileDialog1.FileName;			
			byte[] byteArray;
			try
			{
				using (FileStream fstream = File.OpenRead("new.png"))
				{
					byteArray = new byte[fstream.Length];
					fstream.Read(byteArray, 0, byteArray.Length);
					Bitmap bmp = new Bitmap(fstream);
					pictureBox1.Image = bmp;
				}
				using (FileStream fstream = new FileStream("test.png",FileMode.Create))
				{
					fstream.Write(byteArray, 0, byteArray.Length);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			button1_Click(sender, e);
		}
	}
}
