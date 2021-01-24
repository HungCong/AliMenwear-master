using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace project_sem_3.Models
{
    public class BasePhoto
    {
        public string? Thumbnails { get; set; }
        public string GetDefaultThumbnail()
        {
            if (Thumbnails != null && this.Thumbnails.Length > 0)
            {
                var arrayThumbnail = this.Thumbnails.Split(',');
                if (arrayThumbnail.Length > 0)
                {
                    if (IsUrlValid(arrayThumbnail[0]))
                    {
                        return arrayThumbnail[0];
                    }
                    return ConfigurationManager.AppSettings["CloudinaryPrefix"] + arrayThumbnail[0];
                }
            }
            return ConfigurationManager.AppSettings["DefaultImage"];
        }

        private bool IsUrlValid(string url)
        {
            string pattern = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        public List<string> GetThumbnails()
        {
            List<string> ls = new List<string>();
            if (Thumbnails != null && this.Thumbnails.Length > 0)
            {

                var arrayThumbnail = this.Thumbnails.Split(',');
                if (arrayThumbnail.Length > 0)
                {
                    for (var i = 0; i < arrayThumbnail.Length; i++)
                    {
                        if (IsUrlValid(arrayThumbnail[0]))
                        {
                            ls.Add(arrayThumbnail[i]);
                        }
                        else
                        {
                            ls.Add("https://res.cloudinary.com/dyi6c1dgi/image/upload/c_limit,h_60,w_90/v1588171812/pj-sem-3/" + arrayThumbnail[i] + ".jpg");
                        }
                    }
                    return ls;
                }
            }
            return ls;
        }

        public string[] GetThumbnailIds()
        {
            var ids = new List<string>();
            var thumbnails = GetThumbnails();
            foreach (var thumb in thumbnails)
            {
                var splittedThumb = thumb.Split('/');
                if (splittedThumb.Length != 5)
                {
                    continue;
                }
                ids.Add(splittedThumb[4].Split('.')[0]);
            }
            return ids.ToArray();
        }
    }
}