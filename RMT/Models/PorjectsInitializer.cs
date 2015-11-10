using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RMT.Models
{
    public class PorjectsInitializer : DropCreateDatabaseAlways<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {
            base.Seed(context);

            var Projects = new List<Project>
            {
                new Project {
                    Name= "BMW Type 12",
                    Description = "Lorem ipsum", 
                    Status = "En cours de réalisation",
                    BeginDate = Convert.ToDateTime("12/10/2015"),
                    ImagePath = "~/Content/Images/Projects/Project1/Title.jpg"
                },
                new Project {
                    Name= "BMW RTM V8",
                    Description = "Lorem ipsum", 
                    Status = "En cours de conception",
                    ImagePath = "~/Content/Images/Projects/Project2/Title.jpg"
                }
            };

            Projects.ForEach(s => context.Projects.Add(s));
            context.SaveChanges();

            var Pictures = new List<Picture>
            {
                new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo1.jpg"
                },
                new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo2.jpg"
                },
                new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo3.jpg"
                },
                 new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo4.jpg"
                },
                 new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo5.jpg"
                },
                 new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo6.jpg"
                },
                 new Picture {
                    ProjectId = 1,
                    PictureName = "Lorem Ipsum",
                    Description = "Bla bla bla bla bla bla bla ",
                    Path = "~/Content/Images/Projects/Project2/Photo7.jpg"
                }
            };

            Pictures.ForEach(p => context.Pictures.Add(p));
            context.SaveChanges();

        }
    }
}