﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using System.Text.RegularExpressions;

namespace KoyaLawWeb
{
    public partial class WebForm3 : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

            string values = "11,28,32,410,51,669,52,670,53,54,411,55,672,56,673,57,58,412,59,4232,5302,41311,33,413,510,626,511,512,513,514,628,414,515,629,516,517,518,519,631,415,520,4233,5303,41312,34,416,417,418,521,4282,5350,4294,5388,41350,35,419,5192,420,5195,421,5196,4235,5305,41313,36,422,5197,423,5198,4240,5310,41314,29,38,424,5159,5399,5400,6157,6158,5416,4180,5208,5401,5402,6159,6160,5417,4198,5263,4236,5306,41344,39,425,5213,5403,5404,6161,6162,5418,4184,5214,5405,5406,6163,6164,5419,4199,5264,4237,5307,41345,310,426,5219,5407,5408,6165,6166,5420,4188,5220,5409,5410,6167,6168,5421,4200,5265,4238,5308,41346,311,427,5181,5182,6155,5183,667,5411,6169,6170,5422,4192,5239,5240,6156,5241,668,5412,6171,6172,5423,4201,5266,4239,5309,41347,312,428,5256,5413,6173,6174,5424,4196,5257,5414,6175,6176,5425,4223,5290,4241,5311,41349,364,4308,223,313,435,5145,5146,5147,665,5148,651,436,5150,5151,5152,666,5153,653,437,5155,4242,5312,41315,314,438,5249,439,5250,315,440,5140,622,5141,623,441,5142,624,5143,625,442,5144,4243,5313,41316,316,443,549,550,551,552,633,444,553,554,555,556,634,445,5139,4244,5314,41317,317,446,545,546,635,447,547,548,636,448,5138,4245,5315,41318,318,449,5131,5132,5133,450,5134,5135,5136,451,5137,4246,5316,41319,319,452,541,542,543,544,638,453,5130,4247,5317,41320,320,454,539,639,455,540,640,456,5129,4248,5318,41321,326,475,5112,476,5113,477,5114,478,5115,479,5116,480,5117,481,5118,4249,5319,41325,362,4284,5354,5355,5356,681,682,683,684,685,686,687,688,5357,689,4285,5358,5359,5360,690,691,692,693,694,695,696,697,5361,698,4286,5362,5363,5364,699,6100,6101,6102,6103,6104,6105,6106,6107,6108,5365,6109,4287,5366,5367,5368,6110,6111,6112,6113,6114,6115,6116,6117,5369,6118,4288,5370,5371,5372,6119,6120,6121,6122,6123,6124,6125,6126,5373,6127,4289,5374,5375,5376,6128,6129,6130,6131,6132,6133,6134,6135,6136,6137,5377,6138,4290,5378,4295,5415,41352,363,4291,5379,5380,5381,6139,6140,5382,6141,6142,6143,4292,5383,5384,5385,6144,6145,5386,6146,6147,6148,4304,5435,4305,5434,41357,365,4309,51001,51002,4311,51005,51006,366,4310,51003,51004,4312,51007,51008,224,322,460,461,462,463,464,5125,4250,5320,4280,41322,226,325,470,5119,5295,675,471,5120,472,5121,5296,676,473,5122,474,5123,4252,5322,41324,227,327,482,5109,5110,483,5111,4253,5323,41326,328,484,5107,485,5108,4271,5341,41327,329,486,5106,487,5105,4254,5324,41328,330,488,5251,489,5288,4255,5325,41329,331,490,5267,491,5104,4256,5326,41330,228,332,492,5103,4298,5428,4299,5429,41354,333,493,522,523,524,61,525,62,494,526,527,528,63,529,64,495,594,4257,5327,41331,334,496,5204,231,358,4229,12,21,336,4104,4105,4106,557,4258,5328,41332,338,4112,4113,4114,559,4259,5329,41334,339,4115,4116,4117,4118,4119,560,4260,5330,41335,340,4120,4121,4128,578,4261,5331,41336,341,4122,579,580,620,4123,581,582,621,4300,5430,4301,5431,41355,342,4124,583,584,618,4125,585,586,619,4302,5432,4303,5433,41356,343,4126,587,4127,588,4195,5248,4262,5332,41343,345,4131,5247,4133,574,4134,4135,4136,4137,4138,4139,4140,5188,4141,5189,4142,575,4263,5333,41337,351,4170,4171,4172,4173,4283,5351,4293,5387,41351,361,4281,4296,5427,4297,5426,41353,22,344,4129,576,4130,577,4222,5289,4264,5334,41348,352,4174,5262,4175,561,4176,4265,5335,41342,23,337,4107,4108,4109,4110,4111,558,4266,5336,41333,346,4143,4144,4145,4146,569,4147,570,4148,571,4149,572,4267,5337,41338,350,4163,562,4164,563,4165,564,4166,565,4167,4168,4169,566,4268,5338,41341,24,348,4154,5245,4155,5246,4156,4157,568,4269,5339,41339,25,349,4158,5242,4159,5243,4160,4161,5244,4162,567,4270,5340,41340,26,347,4150,4151,4152,4153,4306,5436,4307,5437,41358,232,359,4230,13,225,323,465,5206,466,5207,324,467,530,65,66,531,643,644,532,67,68,5258,69,610,5260,647,648,468,533,611,612,534,645,646,535,613,614,5259,615,616,5261,649,650,469,536,4251,5321,41323,229,354,4206,4207,5274,5352,4208,5275,4209,5276,661,662,663,6152,6153,6154,5277,664,4210,5278,5392,4211,5279,5393,4278,5348,5394,355,4272,4273,5342,5353,4274,5343,4275,5344,677,678,679,6149,6150,6151,5345,680,4276,5346,5389,4277,5347,5390,4279,5349,5391,230,356,4217,4218,5284,4219,5285,5395,4220,5286,5396,4221,5287,357,4224,4225,5291,4226,5292,5397,4227,5293,5398,4228,5294,233,360,4231,";

            string[] subcatstring = Regex.Split(values, ",3");
           

              if(subcatstring.Length>0)
              {
                  Response.Write((subcatstring.Length - 1).ToString());
              }
       



            //DataTable dtt = DataAccess.ISDAAgreement.AllISDAAgreements();
            //if (dtt != null && dtt.Rows.Count > 0)
            //{
            //  foreach(DataRow item in dtt.Rows)
            //  {
            //    int agreementID = Convert.ToInt32(item["ClientAgreementID"].ToString());
                
            //       DataTable dt2 = DataAccess.ISDAAgreement.GetallCounterpartyQuestionsfromISDA(agreementID);
            //       if(dt2 != null && dt2.Rows.Count > 0)
            //       {


                       

            //           questiongrid.DataSource = dt2;
            //           questiongrid.DataBind();
            //           //string Counterpartyname = dt2.Rows[0]["Answer"].ToString();
            //           //DataTable dt3 = DataAccess.ISDAAgreement.GetCounterPartiesIDbyCounterPartyName(Counterpartyname);
            //           //if(dt3 != null && dt3.Rows.Count > 0)
            //           //{
            //           //    int CounterPartyID = Convert.ToInt32(dt3.Rows[0]["CounterPartyID"].ToString());
            //           //    int status1 = DataAccess.ISDAAgreement.UpdateCounterpartyIDtoQuestionsfromISDA(agreementID,1,CounterPartyID);
            //           //      int status2 = DataAccess.ISDAAgreement.UpdateCounterpartyIDtoQuestionsfromISDA(agreementID,2,CounterPartyID);
            //           //}
            //       }
            //   }
            //} 
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            //DataTable dtt = DataAccess.ISDAAgreement.AllISDAAgreements();
            //if (dtt != null && dtt.Rows.Count > 0)
            //{
            //  foreach(DataRow item in dtt.Rows)
            //  {
            //    int agreementID = Convert.ToInt32(item["ClientAgreementID"].ToString());
                
            //       DataTable dt2 = DataAccess.ISDAAgreement.GetallCounterpartyQuestionsfromISDA(agreementID);
            //       if(dt2 != null && dt2.Rows.Count > 0)
            //       {
            //           questiongrid.DataSource = dt2;
            //           questiongrid.DataBind();
            //           string Counterpartyname = dt2.Rows[0]["Answer"].ToString();
            //           DataTable dt3 = DataAccess.ISDAAgreement.GetCounterPartiesIDbyCounterPartyName(Counterpartyname);
            //           if(dt3 != null && dt3.Rows.Count > 0)
            //           {
            //               int CounterPartyID = Convert.ToInt32(dt3.Rows[0]["CounterPartyID"].ToString());
            //               int status1 = DataAccess.ISDAAgreement.UpdateCounterpartyIDtoQuestionsfromISDA(agreementID,1,CounterPartyID);
            //                 int status2 = DataAccess.ISDAAgreement.UpdateCounterpartyIDtoQuestionsfromISDA(agreementID,2,CounterPartyID);
            //           }
                      
            //       }
            //   }
            //} 
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(3000);
            //Label1.Text = DateTime.Now.ToString();

            System.Threading.Thread.Sleep(5000);
        
        }
    }
}