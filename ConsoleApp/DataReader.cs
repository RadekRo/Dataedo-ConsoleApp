namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {
        IEnumerable<ImportedObject> ImportedObjects { get; set; }

        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
             var importedObjects = new List<ImportedObject>();

            using (var streamReader = new StreamReader(fileToImport))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var values = line.Split(';').Select(value => value.Trim().Replace(" ", "").Replace(Environment.NewLine, "")).ToArray();
                    if (values.Length >= 6) 
                    {
                        var importedObject = new ImportedObject
                        {
                            Type = values[0].ToUpper(),
                            Name = values[1],
                            Schema = values[2],
                            ParentName = values[3],
                            ParentType = values[4],
                            DataType = values[5],
                            IsNullable = values.Length > 6 ? values[6] : "0", 
                            NumberOfChildren = 0
                        };
                        importedObjects.Add(importedObject);
                    }
                }
            }

            var childCounts = importedObjects
                .GroupBy(obj => new { obj.ParentName, obj.ParentType }) 
                .Where(g => !string.IsNullOrEmpty(g.Key.ParentName) && !string.IsNullOrEmpty(g.Key.ParentType))
                .Select(g => new
                {
                    ParentName = g.Key.ParentName,
                    ParentType = g.Key.ParentType.ToUpper(),
                    Count = g.Count()
                }).ToList();

            foreach (var parent in importedObjects)
            {
                var match = childCounts.FirstOrDefault(cc => cc.ParentName == parent.Name && cc.ParentType == parent.Type.ToUpper());
                if (match != null)
                {
                    parent.NumberOfChildren = match.Count;
                }
            }

            ImportedObjects = importedObjects;

            if(printData)
            {
                foreach (var database in ImportedObjects.Where(obj => obj.Type == "DATABASE"))
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    foreach (var table in ImportedObjects.Where(obj => obj.ParentType.ToUpper() == "DATABASE" && obj.ParentName == database.Name))
                    {
                        Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                        foreach (var column in ImportedObjects.Where(obj => obj.ParentType.ToUpper() == "TABLE" && obj.ParentName == table.Name))
                        {
                            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }

    class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }

        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public int NumberOfChildren { get; set; }
    }

    class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
