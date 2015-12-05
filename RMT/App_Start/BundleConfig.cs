using System.Web;
using System.Web.Optimization;

namespace RMT
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/fileinput.js"));

            bundles.Add(new StyleBundle("~/bundles/colorbox").Include(
                      "~/Scripts/colorbox/jquery.colorbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/jalert").Include(
                     "~/Scripts/jAlert/jAlert-v3.js",
                     "~/Scripts/jAlert/jAlert-functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/MyScripts").Include(
                     "~/Scripts/MyScripts.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/colorbox.css",
                      "~/Scripts/dropzone/css/basic.css",
                      "~/Scripts/dropzone/css/dropzone.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.css",
                      "~/Content/jAlert-v3.css",
                      "~/Content/site.css"));

            
            // Définissez EnableOptimizations sur False pour le débogage. Pour plus d'informations,
            // visitez http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
