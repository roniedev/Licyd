using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Globalization;

namespace BaseLib
{
    public static class ExcelHelper
    {
        public static string ExcelToJson(string path, bool hasHeader = true)
        {
            using var pck = new ExcelPackage();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                pck.Load(stream);
            }

            var ws = pck.Workbook.Worksheets.First();
            var tbl = new DataTable();

            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            var startRow = hasHeader ? 2 : 1;

            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];

                // Verifique se todas as células na linha estão em branco
                if (wsRow.All(cell => string.IsNullOrWhiteSpace(cell.Text)))
                {
                    continue; // Pule a linha em branco
                }

                DataRow row = tbl.Rows.Add();

                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            return JsonConvert.SerializeObject(tbl);
        }

        public static string ExcelToJson(IFormFile file, bool hasHeader = true)
        {
            using var pck = new ExcelPackage();
            var filename = Path.GetTempFileName();

            using (var stream = File.Create(filename))
            {
                file.CopyTo(stream);
            }

            var json = ExcelToJson(filename, hasHeader);
            return json;
        }

        public static List<T> ExcelToList<T>(IFormFile file) where T : class
        {
            var json = ExcelToJson(file);

            var dados = JsonConvert.DeserializeObject<List<T>>(json, new JsonSerializerSettings
            {
                Culture = new CultureInfo("pt-BR"),
                Converters = [new CustomConverter()]
            });

            return dados;
        }

        public static byte[] GetLayout<T>(bool isAddId = false, string worksheetName = "Planilha1") where T : class
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);

            var instance = (T)Activator.CreateInstance(typeof(T));

            var props = instance.GetType().GetProperties();

            int columnIndex = 1;

            if (isAddId)
            {
                worksheet.Cells[1, columnIndex].Value = "Id";
                columnIndex++;
            }

            foreach (var prop in props)
            {
                if (prop.Name != "Id" && prop.Name != "IsSuccess" && prop.Name != "Message")
                {
                    worksheet.Cells[1, columnIndex].Value = prop.Name;
                    columnIndex++;
                }
            }

            byte[] fileBytes = package.GetAsByteArray();
            return fileBytes;
        }

        public class CustomConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(bool) || objectType == typeof(long) ||
                       objectType == typeof(DateTime) || objectType == typeof(decimal) ||
                       objectType == typeof(float) || objectType == typeof(double) ||
                       objectType == typeof(bool?);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var value = (string)reader.Value;
                var propNome = reader.Path[4..];

                try
                {
                    if (objectType == typeof(bool) || objectType == typeof(bool?))
                    {
                        if (value == "1" || value?.ToUpper() == "S" || value?.ToUpper() == "SIM" || value?.ToUpper() == "Y" || value?.ToUpper() == "YES")
                        {
                            return true;
                        }
                        else if (value == "0" || value?.ToUpper() == "N" || value?.ToUpper() == "NÃO" || value?.ToUpper() == "NAO" || value?.ToUpper() == "NO")
                        {
                            return false;
                        }

                        return false;
                    }
                    else if (objectType == typeof(long))
                    {
                        return long.Parse(value);
                    }
                    else if (objectType == typeof(DateTime))
                    {
                        return DateTime.Parse(value);
                    }
                    else if (objectType == typeof(decimal))
                    {
                        return decimal.Parse(value);
                    }
                    else if (objectType == typeof(float))
                    {
                        return float.Parse(value);
                    }
                    else if (objectType == typeof(double))
                    {
                        return double.Parse(value);
                    }
                }
                catch (Exception)
                {
                    throw new ErrorExcelConvertData($"A informação enviada para a coluna {propNome} é inválida.");
                }

                return null;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
