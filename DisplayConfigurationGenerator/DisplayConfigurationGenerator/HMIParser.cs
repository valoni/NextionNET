using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayConfigurationGenerator.Models.HMI;

namespace DisplayConfigurationGenerator
{
    public class HMIParser
    {
        private const uint BlockSizePageData = 24;
        private const uint BlockSizeComponentData = 42;
        private const uint BlockSizeComponentAttributesIndex = 6;
        private const uint BlockSizeFontProperties = 27;
        private const uint BlockSizeImageProperies = 23;

        private byte[] Data;
        private uint StartPositionPageData, StartPositionComponentData, StartPositionComponentAttributes, StartPositionComponentAttributesIndexData;
        private uint StartPositionFontProperties, StartPositionFontData;
        private uint StartPositionImageProperties, StartPositionImageData;

        public HMIParser(byte[] data)
        {
            Data = data;
        }

        public Models.HMI.Display Parse()
        {
            if (Data.Length < 0x48)
                return null;

            var fileSize = Data.GetUInt(0x3C);
            if (Data.Length != fileSize)
                return null;

            var display = new Models.HMI.Display()
            {
                OrientationVertical = Data.GetByte(0x0) == 1,
                VersionMajor = Data.GetByte(0x1),
                VersionMinor = Data.GetByte(0x2),
                ResolutionHorizontal = Data.GetUShort(0xc),
                ResolutionVertical = Data.GetUShort(0xe),
                Pages = new List<Models.HMI.Page>(),
                Fonts = new List<Font>(),
                Images = new List<Image>(),
            };

            StartPositionPageData = Data.GetUInt(0x28);
            StartPositionComponentData = Data.GetUInt(0x2c);
            StartPositionComponentAttributes = Data.GetUInt(0x18);
            StartPositionComponentAttributesIndexData = Data.GetUInt(0x38);
            StartPositionFontProperties = Data.GetUInt(0x34);
            StartPositionFontData = Data.GetUInt(0x14);
            StartPositionImageProperties = Data.GetUInt(0x30);
            StartPositionImageData = Data.GetUInt(0x10);

            var numberOfPages = Data.GetUShort(0x1c);
            for (int i = 0; i < numberOfPages; i++)
            {
                Page page = ParsePage(i);
                display.Pages.Add(page);
            }

            var numberOfFonts = Data.GetUShort(0x22);
            for(int i = 0;i < numberOfFonts;i++)
            {
                Font font = ParseFont(i);
                display.Fonts.Add(font);
            }

            var numberOfImages = Data.GetUShort(0x20);
            for(int i = 0;i < numberOfImages;i++)
            {
                Image image = ParseImage(i);
                display.Images.Add(image);
            }

            return display;
        }

        private Image ParseImage(int imageId)
        {
            var imagePropertiesStart = (int)(StartPositionImageProperties + imageId * BlockSizeImageProperies);
            var relativeStartPosition = Data.GetUInt(imagePropertiesStart + 0x5);
            var absoluteStartPosition = (int)(StartPositionImageData + relativeStartPosition);
            var imageDataSize = (int)Data.GetUInt(imagePropertiesStart + 0xD);
            var data = Data.GetBytes(absoluteStartPosition, imageDataSize);
            string str = Encoding.Default.GetString(data);

            var image = new Image()
            {
                Hash = str.GetHashCode().ToString("X8"),
            };
            return image;
        }

        private Font ParseFont(int fontId)
        {
            var fontStart = (int)(StartPositionFontProperties + (fontId * BlockSizeFontProperties));
            var nameLenght = Data.GetUShort(fontStart + 0x11) + 1;
            var relativeStartOfFontData = Data.GetUInt(fontStart + 0x17);
            var absoulteStartOfFontData = (int)(StartPositionFontData + relativeStartOfFontData);
            var font = new Font()
            {
                Name = Data.GetString(absoulteStartOfFontData, nameLenght),
            };

            return font;
        }

        private Page ParsePage(int pageId)
        {
            var pageStart = (int)(StartPositionPageData + pageId * BlockSizePageData);
            var page = new Models.HMI.Page()
            {
                Id = Data.GetString(pageStart + 0x0, 14),
                Components = new List<Component>(),
            };

            var componentStart = Data.GetUShort(pageStart + 0x10);
            var componentEnd = Data.GetUShort(pageStart + 0x12);

            for (int componentId = componentStart; componentId <= componentEnd; componentId++)
            {
                Component component = ParseComponent(componentId);
                page.Components.Add(component);
            }

            return page;
        }

        private Component ParseComponent(int componentId)
        {
            var componentStart = (int)(StartPositionComponentData + componentId * BlockSizeComponentData);
            Component component = new Component()
            {
                Id = Data.GetString(componentStart + 0x0, 14)
            };

            var attributeStart = Data.GetUShort(componentStart + 0x26);
            var attributeEnd = Data.GetUShort(componentStart + 0x28);
            for(int attributeId = attributeStart;attributeId <= attributeEnd;attributeId++)
            {
                byte[] attributeData = ParseAttribute(attributeId);
                if (attributeData.StartsWith("lei"))
                {
                    var ff1 = Array.IndexOf<byte>(attributeData, 0xff);
                    var ff2 = Array.LastIndexOf<byte>(attributeData, 0xff);
                    if (ff1 < 0 || ff2 < 0 || ff1 == ff2)
                        continue;

                    var typeStartFromFF2 = attributeData[ff1 - 1];

                    component.Type = attributeData.GetString(ff2 + typeStartFromFF2, attributeData.Length - (ff2 + typeStartFromFF2));
                }
            }

            return component;
        }

        private byte[] ParseAttribute(int attributeId)
        {
            var attributeStart = (int)(StartPositionComponentAttributesIndexData + attributeId * BlockSizeComponentAttributesIndex);
            var relativeStartPosition = Data.GetUInt(attributeStart + 0x0);
            var size = Data.GetUShort(attributeStart + 0x4);

            var attributeStartPosition = (int)(StartPositionComponentAttributes + relativeStartPosition);
            return Data.GetBytes(attributeStartPosition, size);
        }
    }
}
