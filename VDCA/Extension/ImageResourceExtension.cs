using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Reflection;
using System.Xml;

namespace VDCA.Extension
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension<ImageSource>
    {
        public string Source { set; get; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Source))
            {
                IXmlLineInfoProvider lineInfoProvider = serviceProvider.GetService(typeof(IXmlLineInfoProvider)) as IXmlLineInfoProvider;
                IXmlLineInfo lineInfo = (lineInfoProvider != null) ? lineInfoProvider.XmlLineInfo : new XmlLineInfo();
                throw new XamlParseException("ImageResourceExtension requires Source property to be set", lineInfo);
            }
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            Image image = provideValueTarget.TargetObject as Image;
            if (image is not null)
            {
                Width = (int)image.Width;
                Height = (int)image.Height;
            }
            string assemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;
            /*
             * string name = Source;
             *if (name.ToLower().Contains(".svg"))
             *{
             *   SvgImageSource imageSource = SvgImageSource.FromResource(assemblyName + "." + Source, typeof(ImageResourceExtension).Assembly, Width, Height);
             *   return imageSource;
             *}
             * else
             * {
             */
            return ImageSource.FromResource(assemblyName + "." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            //}
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
        }
    }
}
