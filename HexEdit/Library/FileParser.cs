using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExCSS;
using TestingProject.Controllers;
using TestingProject.Models;

namespace TestingProject.Library
{
    class FileParser
    {
        public List<string> fileTypeFilters;
        public List<string> filePaths;

        public static List<string> RecursiveDirExplore(List<string> paths, List<string> fileTypeFilters)
        {
            List<string> pathList = new List<string>();
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    var filePath = ProcessFile(path, fileTypeFilters);
                    if (path != null)
                        pathList.Add(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path, fileTypeFilters, pathList);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }
            return pathList;
        }
        public static void RecursiveDirExplore(List<string> paths)
        {
            RecursiveDirExplore(paths, new List<string>());
        }


        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static List<string> ProcessDirectory(string targetDirectory, List<string> fileTypeFilters, List<string> pathList)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                var path = ProcessFile(fileName, fileTypeFilters);
                if (path != null)
                    pathList.Add(path);
            }

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, fileTypeFilters, pathList);

            return pathList;
        }

        // Insert logic for processing found files here. 
        public static string ProcessFile(string path, List<string> fileTypeFilters)
        {
            if (fileTypeFilters.Count > 0)
            {
                return path;
            }
            else if (fileTypeFilters.Any(filter => filter == Path.GetExtension(path)))
            {
                return path;
            }
            else
            {
                return null;
            }
            Console.WriteLine("Processed file '{0}'.", path);
        }

        public static List<StyleSheet> getStyleSheets(List<string> paths)
        {
            Parser parser = new Parser();
            List<StyleSheet> styleSheets = new List<StyleSheet>();
            List<string> fileTypes = new List<string> {".css"};
            List<string> filePaths = RecursiveDirExplore(paths, fileTypes);

            foreach (String fileName in filePaths)
            {
                //styleSheets.Add(parser.Parse(fileName));

                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        String fileText = sr.ReadToEnd();
                        styleSheets.Add(parser.Parse(fileText, Path.GetFileName(fileName)));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
            
            return styleSheets;
        }
        public static List<HexNode> getHexNodes(List<string> filePaths, List<string> cssProperties, List<string> cssFilterValues)
        {
            List<StyleSheet> styleSheets = getStyleSheets(filePaths);
            List<HexNode> hexNodes = new List<HexNode>();

            var parser = new Parser();
            var stylesheet = parser.Parse(".someClass{color: red; background-image: url('/images/logo.png')");

            List<Property> colorProperties = new List<Property>();//styleSheets.SelectMany(s => s.StyleRules).Select(r => r.Declarations)
            //    .SelectMany(d => d.Properties).ToList<Property>()
            //    .Where(p => cssProperties.Contains(p.Name.ToString()) && p.Term != null).ToList<Property>();
                //.Where(p => p.Term != null).ToList<Property>();

            foreach (StyleSheet styleSheet in styleSheets)
            {
                List<Property> styleSheetColorProperties = styleSheet.StyleRules.Select(r => r.Declarations)
                .SelectMany(d => d.Properties).ToList<Property>()
                .Where(p => cssProperties.Contains(p.Name.ToString()) && p.Term != null).ToList<Property>();

                foreach (Property prop in styleSheetColorProperties)
                {
                    string hexVal = convertToHex(prop.Term.ToString(), cssFilterValues);
                    hexNodes.Add(new HexNode(hexVal, prop.Name, styleSheet.Name, prop.LineNumber));
                }

            }

            //List<Property> colorProperties = stylesheet.StyleRules.Select(r => r.Declarations)
                //.SelectMany(d => d.Properties).ToList<Property>();
                //.Where(p => cssProperties.Contains(p.Name.ToString())).ToList<Property>();
                //.Where(p => p.Name.Equals("color", StringComparison.InvariantCultureIgnoreCase))).ToList<Property>();

            // .Where(d => d.Name.Equals("color", StringComparison.InvariantCultureIgnoreCase)).ToList<StyleDeclaration>();

            //foreach (Property prop in colorProperties)
            //{
            //    string hexVal = convertToHex(prop.Term.ToString(), cssFilterValues);
            //    hexNodes.Add(new HexNode(hexVal, prop.Name, prop.LineNumber));
            //}

            return hexNodes;
        }

        public static string convertToHex(string input, List<string> cssFilterValues)
        {
            string result = input;
            if (HexController.webColors.Any(c => string.Equals(c, input, StringComparison.OrdinalIgnoreCase)))
            {
                int ColorValue = Color.FromName(input).ToArgb() & 0xFFFFFF; //remove alpha component
                result = string.Format("{0:x6}", ColorValue);
            }
            if (cssFilterValues.Any(f => string.Equals(input, f, StringComparison.OrdinalIgnoreCase)))
                result = "#FFFFFF";
            return result;
        }
        
    }
}
