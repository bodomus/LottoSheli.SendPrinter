using LottoSheli.SendPrinter.Settings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    public class OcrSettingsAdapter : BaseAdapter<OcrSettings>, IOcrSettings
    {
        private const string OCR_VERSION = "V2";
        protected override string _fileName => "OcrSettings";
        
        public override void Load()
        {
            
            if (_settingsStore.HasSettings(_fileName))
                _currentSettings = _settingsStore.GetSettings<OcrSettings>(_fileName);
            else if (File.Exists(_settingsFile))
            {
                var allVersions = _settingsHelper.LoadJson<Dictionary<string, OcrSettings>>(_settingsFile);
                var version = SettingsManager.OcrSettingsVersion ?? OCR_VERSION;
                if (allVersions.TryGetValue(version, out OcrSettings currentSettings))
                    _currentSettings = currentSettings;
            }
            
        }

        public void SaveOcrSettings()
        {
            Save();
            var allVersions = _settingsHelper.LoadJson<Dictionary<string, OcrSettings>>(_settingsFile);
            allVersions[SettingsManager.OcrSettingsVersion] = _currentSettings;
            _settingsHelper.SaveJson(_settingsFile, allVersions);
        }

        public int HorBlobsFillThreshold
        {
            get
            {
                return _currentSettings.HorBlobsFillThreshold;
            }
            set
            {
                _currentSettings.HorBlobsFillThreshold = value;
            }
        }

        public int HorBlobsHeightThreshold
        {
            get
            {
                return _currentSettings.HorBlobsHeightThreshold;
            }
            set
            {
                _currentSettings.HorBlobsHeightThreshold = value;
            }
        }

        public int MaxGapSize
        {
            get
            {
                return _currentSettings.MaxGapSize;
            }
            set
            {
                _currentSettings.MaxGapSize = value;
            }
        }

        public int BitmapCropMargin
        {
            get
            {
                return _currentSettings.BitmapCropMargin;
            }
            set
            {
                _currentSettings.BitmapCropMargin = value;
            }
        }

        public int UserIdScaleFactor
        {
            get
            {
                return _currentSettings.UserIdScaleFactor;
            }
            set
            {
                _currentSettings.UserIdScaleFactor = value;
            }
        }

        public int UserIdBlobNormalWidthAfterScaling
        {
            get
            {
                return (UserIdScaleFactor * _currentSettings.UserIdBlobNormalWidth) / 3; 
            }
        }

        public int UserIdBlobNormalHeightAfterScaling
        {
            get
            {
                return (UserIdScaleFactor * _currentSettings.UserIdBlobNormalHeight) / 3;
            }
        }

        public int UserIdLength
        {
            get
            {
                return _currentSettings.UserIdLength;
            }
            set
            {
                _currentSettings.UserIdLength = value;
            }
        }

        public int BarcodeSpaceWidthThreshold
        {
            get
            {
                return _currentSettings.BarcodeSpaceWidthThreshold;
            }
            set
            {
                _currentSettings.BarcodeSpaceWidthThreshold = value;
            }
        }

        public int BarcodeMinNumberOfSpaces
        {
            get
            {
                return _currentSettings.BarcodeMinNumberOfSpaces;
            }
            set
            {
                _currentSettings.BarcodeMinNumberOfSpaces = value;
            }
        }

        public int BarcodeNumOfSpacesToScan
        {
            get
            {
                return _currentSettings.BarcodeNumOfSpacesToScan;
            }
            set
            {
                _currentSettings.BarcodeNumOfSpacesToScan = value;
            }
        }

        public int BarcodeExpectedDataLength
        {
            get
            {
                return _currentSettings.BarcodeExpectedDataLength;
            }
            set
            {
                _currentSettings.BarcodeExpectedDataLength = value;
            }
        }

        public int TopBarcodeMinDataLength
        {
            get
            {
                return _currentSettings.TopBarcodeMinDataLength;
            }
            set
            {
                _currentSettings.TopBarcodeMinDataLength = value;
            }
        }

        public int TopOffsetThreshold
        {
            get
            {
                return _currentSettings.TopOffsetThreshold;
            }
            set
            {
                _currentSettings.TopOffsetThreshold = value;
            }
        }

        public int BottomBarcodeHeight
        {
            get
            {
                return _currentSettings.BottomBarcodeHeight;
            }
            set
            {
                _currentSettings.BottomBarcodeHeight = value;
            }
        }

        public int MinExtraDigitSize
        {
            get
            {
                return _currentSettings.MinExtraDigitSize;
            }
            set
            {
                _currentSettings.MinExtraDigitSize = value;
            }
        }

        public int ExtraScaleFactor
        {
            get
            {
                return _currentSettings.ExtraScaleFactor;
            }
            set
            {
                _currentSettings.ExtraScaleFactor = value;
            }
        }

        public float ExtraMarkerAspect
        {
            get
            {
                return _currentSettings.ExtraMarkerAspect;
            }
            set
            {
                _currentSettings.ExtraMarkerAspect = value;
            }
        }

        public float ExtraMarkerAngleTolerance
        {
            get
            {
                return _currentSettings.ExtraMarkerAngleTolerance;
            }
            set
            {
                _currentSettings.ExtraMarkerAngleTolerance = value;
            }
        }

        public int ExtraBlobFilterMinSize
        {
            get
            {
                return _currentSettings.ExtraBlobFilterMinSize;
            }
            set
            {
                _currentSettings.ExtraBlobFilterMinSize = value;
            }
        }

        public int UserIdBoxIndex
        {
            get
            {
                return _currentSettings.UserIdBoxIndex;
            }
            set
            {
                _currentSettings.UserIdBoxIndex = value;
            }
        }

        public int CropLineBoxIndex
        {
            get
            {
                return _currentSettings.CropLineBoxIndex;
            }
            set
            {
                _currentSettings.CropLineBoxIndex = value;
            }
        }

        public int UserIdContrastApplyLevel
        {
            get
            {
                return _currentSettings.UserIdContrastApplyLevel;
            }
            set
            {
                _currentSettings.UserIdContrastApplyLevel = value;
            }
        }

        public int UserIdPaddingX
        {
            get
            {
                return _currentSettings.UserIdPaddingX;
            }
            set
            {
                _currentSettings.UserIdPaddingX = value;
            }
        }

        public int UserIdPaddingY
        {
            get
            {
                return _currentSettings.UserIdPaddingY;
            }
            set
            {
                _currentSettings.UserIdPaddingY = value;
            }
        }

        public GoogleOcrSettings GoogleOcrSettings { get => _currentSettings.GoogleOcrSettings; set => _currentSettings.GoogleOcrSettings = value; }

        public int UserIdMaximalHeight { get => _currentSettings.UserIdMaximalHeight; set => _currentSettings.UserIdMaximalHeight = value; }

        public int UserIdMinimallHeight { get => _currentSettings.UserIdMinimallHeight; set => _currentSettings.UserIdMinimallHeight = value; }

        public int UserIdMaximalWidth { get => _currentSettings.UserIdMaximalWidth; set => _currentSettings.UserIdMaximalWidth = value; }

        public int UserIdMinimallWidth { get => _currentSettings.UserIdMinimallWidth; set => _currentSettings.UserIdMinimallWidth = value; }

        public string TessnetPath { get => _currentSettings.TessnetPath; set => _currentSettings.TessnetPath = value; }
        public string TessnetV4Path { get => _currentSettings.TessnetV4Path; set => _currentSettings.TessnetV4Path = value; }

        public int ExtraMarkerHeightBottomLimit { get => _currentSettings.ExtraMarkerHeightBottomLimit; set => _currentSettings.ExtraMarkerHeightBottomLimit = value; }

        public int ExtraMarkerWidthBottomLimit { get => _currentSettings.ExtraMarkerWidthBottomLimit; set => _currentSettings.ExtraMarkerWidthBottomLimit = value; }

        public int ExtraMarkerWidthTopLimit { get => _currentSettings.ExtraMarkerWidthTopLimit; set => _currentSettings.ExtraMarkerWidthTopLimit = value; }
        
        public int TicketThreshold { get => _currentSettings.TicketThreshold; set => _currentSettings.TicketThreshold = value; }
        
        public int NationalIdThreshold { get => _currentSettings.NationalIdThreshold; set => _currentSettings.NationalIdThreshold = value; }

        public int? BottomCropLineBoxReverseIndex { get => _currentSettings.BottomCropLineBoxReverseIndex; set => _currentSettings.BottomCropLineBoxReverseIndex = value; }

        public string OcrSettingsProfileName => SettingsManager.OcrSettingsVersion;

        public string Description => _currentSettings.Description;

        public int BottomCropLineOffset { get => _currentSettings.BottomCropLineOffset; set => _currentSettings.BottomCropLineOffset = value; }

        public RecognitionStrategiesSettings RecognitionStrategies { get => _currentSettings.RecognitionStrategies; set => _currentSettings.RecognitionStrategies = value; }

        public int BottomBarcodeYOffset { get => _currentSettings.BottomBarcodeYOffset; set => _currentSettings.BottomBarcodeYOffset = value; }

        public int BottomBarcodeXMargin { get => _currentSettings.BottomBarcodeXMargin; set => _currentSettings.BottomBarcodeXMargin = value; }

        public string SlipTemplatePath { get => _currentSettings.SlipTemplatePath; set => _currentSettings.SlipTemplatePath = value; }

        public string UnrecognizedScanPath { get => _currentSettings.UnrecognizedScanPath; set => _currentSettings.UnrecognizedScanPath = value; }
    }
}
