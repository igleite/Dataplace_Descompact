using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dataplace_Descompact
{
    internal class ProcessaDiretorios
    {
        private bool DeletaCompactados { get; set; }

        private bool VarreSubDiretorios { get; set; }

        private bool RenomearDefault { get; set; }

        public ProcessaDiretorios(bool deletaCompactados, bool varreSubDiretorios, bool renomearDefault)
        {
            DeletaCompactados = deletaCompactados;
            VarreSubDiretorios = varreSubDiretorios;
            RenomearDefault = renomearDefault;
        }

        public void CarregaDiretorios(Panel panel, ListBox listagem, TextBox currentDirectory, ProgressBar progressBar1)
        {
            currentDirectory.ResetText();
            progressBar1.Value = 0;
            foreach (object item in listagem.Items)
            {
                string[] directories = Directory.GetDirectories(item.ToString(), "*", SearchOption.AllDirectories);
                if (VarreSubDiretorios)
                {
                    string[] array = directories;
                    foreach (string text in array)
                    {
                        ProcessaDescompacta(text, panel, currentDirectory, progressBar1);
                    }
                }
            }

            if (progressBar1.Value == 100)
            {
                MessageBox.Show("Arquivos extraídos com sucesso", "Informação", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }

            panel.Hide();
        }

        private void ProcessaDescompacta(string path, Panel panel, TextBox currentDirectory, ProgressBar progressBar1)
        {
            currentDirectory.Visible = true;
            panel.Visible = true;
            List<string> list = (from x in Directory.GetFiles(path)
                where x.Contains(".rar")
                select x).ToList();


            if (!list.Any())
            {
                currentDirectory.AppendText("Não há arquivos para extrair no diretório: '" +
                                            path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal) + 1) + "'" +
                                            Environment.NewLine);
                panel.Hide();
                return;
            }

            currentDirectory.AppendText(path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal) + 1) +
                                        " => OK" + Environment.NewLine);


            var dictionary = new Dictionary<string, object>();

            list.ForEach(delegate(string x)
            {
                RarArchive val = RarArchive.Open(x);
                foreach (RarArchiveEntry item in val.Entries.Where(entry => !entry.IsDirectory))
                {
                    progressBar1.Value = 0;

                    if (dictionary.Count == 0)
                    {
                        dictionary.Add(item.Key, item.LastModifiedTime);

                        item.WriteToDirectory(path,
                            new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                PreserveFileTime = true,
                                Overwrite = true
                            });
                    }
                    else
                    {
                        var encontrou = dictionary.FirstOrDefault(t => t.Key == item.Key).Key;

                        if (!string.IsNullOrEmpty(encontrou))
                        {
                            var ano1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Year")
                                .GetValue(item.LastModifiedTime, null);

                            var mes1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Month")
                                ?.GetValue(item.LastModifiedTime, null);

                            var dia1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Day")
                                ?.GetValue(item.LastModifiedTime, null);

                            var hora1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Hour")
                                ?.GetValue(item.LastModifiedTime, null);

                            var minuto1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Minute")
                                ?.GetValue(item.LastModifiedTime, null);

                            var segundo1 = item.LastModifiedTime
                                .GetType()
                                .GetProperty("Second")
                                ?.GetValue(item.LastModifiedTime, null);

                            var ano2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Year")
                                ?.GetValue(dictionary[item.Key], null);

                            var mes2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Month")
                                .GetValue(dictionary[item.Key], null);

                            var dia2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Day")
                                ?.GetValue(dictionary[item.Key], null);

                            var hora2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Hour")
                                ?.GetValue(dictionary[item.Key], null);

                            var minuto2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Minute")
                                ?.GetValue(dictionary[item.Key], null);

                            var segundo2 = dictionary[item.Key]
                                .GetType()
                                .GetProperty("Second")
                                ?.GetValue(dictionary[item.Key], null);


                            DateTime date1 = new DateTime((int)ano1,
                                (int)mes1,
                                (int)dia1,
                                (int)hora1,
                                (int)minuto1,
                                (int)segundo1);

                            DateTime date2 = new DateTime((int)ano2,
                                (int)mes2,
                                (int)dia2,
                                (int)hora2,
                                (int)minuto2,
                                (int)segundo2);

                            int result = DateTime.Compare(date1, date2);

                            if (result > 0)
                            {
                                dictionary.Remove(item.Key);
                                dictionary.Add(item.Key, item.LastModifiedTime);

                                IArchiveEntryExtensions.WriteToDirectory((IArchiveEntry)(object)item, path,
                                    new ExtractionOptions
                                    {
                                        ExtractFullPath = true,
                                        PreserveFileTime = true,
                                        Overwrite = true
                                    });
                            }
                        }
                        else
                        {
                            dictionary.Add(item.Key, item.LastModifiedTime);


                            IArchiveEntryExtensions.WriteToDirectory((IArchiveEntry)(object)item, path,
                                new ExtractionOptions
                                {
                                    ExtractFullPath = true,
                                    PreserveFileTime = true,
                                    Overwrite = true
                                });
                        }
                    }

                    progressBar1.Value = 100;
                }

                val.Dispose();
                if (DeletaCompactados)
                {
                    File.Delete(x);
                }

                if (RenomearDefault && path.Contains("Control"))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)

                        if (Path.GetFileName(file) == "Config_default.ini" ||
                            Path.GetFileName(file) == "PAINEL01_NFE_default.INI")
                        {
                            RenameFile(path + "\\" + Path.GetFileName(file), path + "\\" + Path.GetFileName(file).Replace("_default", ""));
                        }
                }
            });
        }

        public void RenameFile(string originalName, string newName)
        {
            File.Move(originalName, newName);
        }
    }
}