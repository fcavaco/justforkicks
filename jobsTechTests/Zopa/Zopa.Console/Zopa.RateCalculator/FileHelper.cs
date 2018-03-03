using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

//NOTE: have coded this class for one of my projects and as this is not core of technical test just reusing it here to save some time.
namespace Zopa.RateCalculator
{
   public class FileHelper
        {
            public static IEnumerable<T> LoadCsv<M, T>(M mapper, Stream stream)
            {
                IEnumerable<T> settings;

                using (var reader = new StreamReader(stream))
                {
                    settings = LoadCsv<M, T>(mapper, reader);
                }
                return settings;
            }

            public static IEnumerable<T> LoadCsv<M, T>(M mapper, StreamReader reader)
            {
                IEnumerable<T> items = null;
                var conf = new Configuration();
                conf.HasHeaderRecord = true;
                conf.IgnoreBlankLines = true;

                conf.RegisterClassMap(mapper as ClassMap<T>);
                using (var csv = new CsvReader(reader, conf))
                {
                    try
                    {

                        items = csv.GetRecords<T>().ToList();
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }
                return items;
            }
            public static IEnumerable<T> LoadCsv<M, T>(M mapper, string file)
            {
                IEnumerable<T> items;
                using (var reader = File.OpenText(file))
                {
                    items = LoadCsv<M, T>(mapper, reader);
                }
                return items;
            }
        
        }
        
    }

