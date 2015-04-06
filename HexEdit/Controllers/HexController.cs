using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingProject.Models;
using TestingProject.Library;

namespace TestingProject.Controllers
{
    public class HexController : Controller
    {
        public int colorDiffMax = 70;
        public int colorDiffMin = 20;
        public List<string> filePaths = new List<string>();
        public static string fileDir = "C:/Users/Aidan Arrowood/WebDev/TestingProject/TestingProject/TestingProject/Content/testcss";
        public List<string> cssProps = new List<string> { "color", "background-color", "background" };
        public List<string> filterPropValues = new List<string> { "inherit", "transparant", "initial", "none"};

        public static string[] webColors = new string[] {"AliceBlue","AntiqueWhite","Aqua","Aquamarine","Azure","Beige","Bisque","Black","BlanchedAlmond","Blue","BlueViolet","Brown","BurlyWood","CadetBlue","Chartreuse","Chocolate","Coral","CornflowerBlue","Cornsilk","Crimson","Cyan","DarkBlue","DarkCyan","DarkGoldenRod","DarkGray","DarkGreen","DarkKhaki","DarkMagenta","DarkOliveGreen","DarkOrange","DarkOrchid","DarkRed","DarkSalmon","DarkSeaGreen","DarkSlateBlue","DarkSlateGray","DarkTurquoise","DarkViolet","DeepPink","DeepSkyBlue","DimGray","DodgerBlue","FireBrick","FloralWhite","ForestGreen","Fuchsia","Gainsboro","GhostWhite","Gold","GoldenRod","Gray","Green","GreenYellow","HoneyDew","HotPink","Ivory","Khaki","Lavender","LavenderBlush","LawnGreen","LemonChiffon","LightBlue","LightCoral","LightCyan","LightGoldenRodYellow","LightGray","LightGreen","LightPink","LightSalmon","LightSeaGreen","LightSkyBlue","LightSlateGray","LightSteelBlue","LightYellow","Lime","LimeGreen","Linen","Magenta","Maroon","MediumAquaMarine","MediumBlue","MediumOrchid","MediumPurple","MediumSeaGreen","MediumSlateBlue","MediumSpringGreen","MediumTurquoise","MediumVioletRed","MidnightBlue","MintCream","MistyRose","Moccasin","NavajoWhite","Navy","OldLace","Olive","OliveDrab","Orange","OrangeRed","Orchid","PaleGoldenRod","PaleGreen","PaleTurquoise","PaleVioletRed","PapayaWhip","PeachPuff","Peru","Pink","Plum","PowderBlue","Purple","RebeccaPurple","Red","RosyBrown","RoyalBlue","SaddleBrown","Salmon","SandyBrown","SeaGreen","SeaShell","Sienna","Silver","SkyBlue","SlateBlue","SlateGray","Snow","SpringGreen","SteelBlue","Tan","Teal","Thistle","Tomato","Turquoise","Violet","Wheat","White","WhiteSmoke","Yellow"};

        public ActionResult Index()
        {
            List<string> filePaths = new List<string>();
            filePaths.Add(fileDir);
            return View(findBlue(filePaths));
        }

        public ActionResult Hex()
        {
            //string[] input = { "0", "#f9e2e7", "607", "#4d8ce1", "0", "#0000FF", "12", "#c75b5b", "149", "#D4D4D4", "488", "#db6464", "1292", "#bf1e2e", "54", "#ededed", "139", "#d4d4d4", "258", "#878787", "260", "#666", "291", "#000", "389", "#fff", "447", "#4b4949", "525", "#b22130", "577", "#db6464", "586", "#c9d2df", "591", "#db6464", "607", "#4d8ce1", "616", "#231f20", "646", "#fff", "665", "#d4d3d3", "671", "#db6464", "689", "#db6464", "696", "#fff", "746", "#999", "748", "#b7b6b6", "749", "#be3140", "751", "#4d8ce1", "759", "#000", "763", "#db6464", "775", "#db6464", "790", "#b22130", "806", "#c3de57", "808", "#bf1e2e", "823", "#3f3e3e", "845", "#231f20", "857", "#fff", "859", "#696767", "864", "#464545", "902", "#c3de57", "960", "#999", "963", "#000", "973", "#000", "1018", "#bf1e2e", "1063", "#474747", "1088", "#fff", "1091", "#b13240", "1107", "#fff", "1110", "#bf1e2e", "1114", "#626161", "1132", "#7c7c7c", "1150", "#646464", "1223", "#be3140", "1227", "#340a0e", "1239", "#f9f9f9", "1304", "#fff", "1305", "#fff", "1316", "#fff", "1318", "#f9e2e7", "1336", "#bf1e2e", "1343", "#bf1e2e", "1430", "#4d8ce1", "1490", "#db6464", "1553", "#e3e3e3", "1561", "#e3e3e3", "1586", "#fff", "1592", "#606162", "1596", "#606162", "1635", "#606162", "1659", "#fff", "1660", "#000", "1691", "#000", "1756", "#070d06", "1759", "#070d06", "1790", "#999", "1807", "#606162", "1840", "#000", "1853", "#000", "1876", "#999", "1948", "#131a60", "1991", "#bf1e2e", "1994", "#bf1e2e", "2028", "#fff", "2030", "#8f0f30", "2049", "#2a46a0", "2089", "#fff", "2206", "#3b3b3b", "2211", "#cccccc", "2315", "#fff", "2344", "#000", "2348", "#db6464", "2354", "#514f4f", "2366", "#747171", "2367", "#908c8c", "2379", "#fff", "2446", "#d1d1d1", "2447", "#01095a", "2461", "#db6464", "2506", "#f9f9fc", "2532", "#000", "2576", "#e14d62", "2579", "#e2e1e1", "2606", "#000", "2613", "#666", "2640", "#4d8ce1", "2706", "#606162", "2709", "#606162", "2720", "#fff", "2757", "#4d8ce1", "2759", "#4d8ce1", "2761", "#000", "2764", "#606162", "2860", "#606162", "2895", "#000", "2914", "#606162", "2941", "#606162", "2944", "#000", "2960", "#606162", "3010", "#b22130", "3018", "#fff", "3081", "#8d8d8e", "3103", "#e2ebf9", "3136", "#d1d1d1", "3137", "#5a010e", "3184", "#000", "3312", "#f9f9fc", "3376", "#999", "3385", "#000", "3395", "#454545", "3454", "#707071", "3471", "#dde8f8", "3472", "#454545", "3484", "#707071", "3488", "#707071", "3509", "#afc969", "3517", "#7e7d7d", "3520", "#454545", "3523", "#4d8ce1", "3532", "#bf1e2e", "3570", "#5a010e", "3571", "#d1d1d1", "3581", "#4d8ce1", "3661", "#8d8d8e", "3677", "#f2f7fd", "3737", "#95132f", "3757", "#bf1e2e", "3766", "#686666", "3811", "#606162", "3833", "#4d8ce1", "3856", "#606162", "3914", "#b22130", "3998", "#fff", "3999", "#000", "4004", "#bf1e2e", "4005", "#081991", "4021", "#bf1e2e", "4026", "#bf1e2e", "4027", "#fff", "4051", "#606162", "4108", "#fff", "4153", "#fff", "4162", "#db6464", "4220", "#464545", "4291", "#4d8ce1", "4336", "#fff", "4350", "#464545", "4365", "#009", "4385", "#4d8ce1", "4397", "#c8c7c7", "4453", "#747373", "4464", "#606162", "4481", "#606162", "4704", "#fff", "4738", "#000", "4751", "#9c222e", "4762", "#606162", "4780", "#f9e3e7", "4790", "#606162", "4826", "#4d8ce1", "4964", "#d7d7d7", "4978", "#e1e1e1", "4984", "#fff", "4989", "#606162", "5022", "#ebebeb", "5130", "#051aaf", "5205", "#a1a1a1", "5228", "#606162", "5245", "#ebebeb", "5351", "#606162", "5382", "#e1e1e1", "5401", "#db6464", "5409", "#0292B5", "5412", "#ebebeb", "5453", "#808080", "5455", "#808080", "5485", "#e2ebf9", "5536", "#bf1e2e", "5537", "#fff", "5543", "#901132", "5571", "#d5d6d6", "5575", "#e14f5d", "5632", "#606162", "5711", "#66afe9", "5727", "#fff", "5728", "#e2e2e2", "5797", "#606162", "5815", "#000", "5834", "#951b35", "5913", "#d5d6d6", "5932", "#e14f5d", "5964", "#c3de57", "6046", "#e14f5d", "6055", "#d5d6d6", "6113", "#000", "6154", "#e14f5d", "6178", "#d5d6d6", "6230", "#bf1e2e", "6237", "#ff0000", "6238", "#ffffff", "6298", "#d5d6d6", "6324", "#66afe9", "6337", "#7f8080", "6353", "#7f8080", "6406", "#d5d6d6", "6497", "#f9f9fc", "6510", "#ffffff", "6549", "#d5d6d6", "6583", "#f9f9fc", "6596", "#ffffff", "6620", "#d5d6d6", "6717", "#000", "6795", "#d5d6d6", "6834", "#bf1e2e", "6883", "#9c222e", "6902", "#000", "6916", "#000", "6958", "#fff", "6975", "#ebebeb", "6983", "#606162", "7020", "#c3de57", "7050", "#929293", "7134", "#000", "7170", "#1f5187", "7171", "#41bffe", "7172", "#f12a8a", "7173", "#c8c8c8", "7174", "#a580fc", "7176", "#dfdfdf", "7249", "#c3de57", "7357", "#4e4e4e", "7460", "#fff", "7471", "#b22130", "7475", "#b22130", "7481", "#f9f9fc", "7549", "#606162", "7594", "#4d8ce1", "7596", "#c75b5b", "7614", "#9c2242", "7652", "#333", "7662", "#bf1e2e", "7692", "#606162", "7710", "#d5d6d6", "7778", "#0f1a8f", "7782", "#808181", "7785", "#e3ecf9", "7841", "#f9f9fc", "7854", "#ffffff", "7889", "#f9f9fc", "7903", "#66afe9", "7916", "#0f1a8f", "7931", "#000", "8532", "#000", "8708", "#d9d8d8", "8729", "#000", "8754", "#ededed", "8822", "#ededed", "8841", "#ededed", "8863", "#d4d4d4", "8916", "#bf1e2e", "8925", "#c7c7c7", "8927", "#e4e4e4", "9028", "#bf1e2e", "9157", "#c0bebe", "9193", "#4d8ce1", "9461", "#131a60", "9848", "#2a48a7", "10175", "#d1e1f6", "10352", "#474747", "10393", "#3f3e3e", "10398", "#d7d7d7", "10480", "#6a6a69", "10769", "#ebebeb", "10811", "#000", "11096", "#c3de57", "11101", "#c3de57", "11867", "#edeeee", "11889", "#1b2695", "12501", "#cc0000", "12760", "#f9f9fc", "12774", "#ffffff" };
            List<string> filePaths = new List<string>();
            filePaths.Add(fileDir);
            return View(findBlue(filePaths));
        }

        public HexData findBlue(List<string> filePaths)
        {

            //string hexValue = decValue.ToString("X");
            //int decValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            //int decValue = Convert.ToInt32(hexValue, 16);
            HexData data = new HexData();
            List<HexNode> hexPairList = FileParser.getHexNodes(filePaths, cssProps, filterPropValues);
            List<HexNode> blueHexList = new List<HexNode>();

            int index = 0;
            foreach (HexNode hexPair in hexPairList)
            {
                index++;
                System.Diagnostics.Debug.WriteLine(index);

                int[] rgb = { 0, 0, 0 };
                bool isBlue = true;

                bool isColorMatch = false;
                //(hexPair.hex.Length-1)/2
                for (int i = 0, j = 0; i < 3; i++)
                {
                    var hexValue = "";
                    if (hexPair.hex.Length < 7 && hexPair.hex.Length >= 4)
                    {
                        hexValue = hexPair.hex.Substring(j+1, 1);
                        hexValue = hexValue + "0";
                        j++;
                    }
                    else if (hexPair.hex.Length >= 4)
                    {
                        hexValue = hexPair.hex.Substring(j+1, 2);
                        j = j + 2;
                    }
                    try
                    {
                        rgb[i] = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(hexPair.hex);
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        
                    }
                }

                if (rgb[2] < rgb[0] || rgb[2] < rgb[1])
                {
                    isBlue = false;
                }
                else if (Math.Abs(rgb[0] - rgb[1]) > colorDiffMax)
                {
                    isBlue = false;
                }
                else if (Math.Abs(rgb[0] - rgb[1]) < colorDiffMin && Math.Abs(rgb[0] - rgb[2]) < colorDiffMin && Math.Abs(rgb[2] - rgb[1]) < colorDiffMin)
                {
                    isBlue = false;
                }

                if (isBlue)
                    blueHexList.Add(hexPair);
            }
            data.hexColors = hexPairList;
            data.selectedHexColors = blueHexList;
            return data;
        }

    }
}
