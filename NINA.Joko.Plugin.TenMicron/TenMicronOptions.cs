﻿#region "copyright"

/*
    Copyright © 2021 - 2021 George Hilios <ghilios+NINA@googlemail.com>

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using NINA.Core.Utility;
using NINA.Joko.Plugin.TenMicron.Interfaces;
using NINA.Joko.Plugin.TenMicron.Model;
using NINA.Profile;
using NINA.Profile.Interfaces;
using System;

namespace NINA.Joko.Plugin.TenMicron {

    public class TenMicronOptions : BaseINPC, ITenMicronOptions {
        private readonly PluginOptionsAccessor optionsAccessor;

        public TenMicronOptions(IProfileService profileService) {
            var guid = PluginOptionsAccessor.GetAssemblyGuid(typeof(TenMicronOptions));
            if (guid == null) {
                throw new Exception($"Guid not found in assembly metadata");
            }

            this.optionsAccessor = new PluginOptionsAccessor(profileService, guid.Value);
            InitializeOptions();
        }

        private void InitializeOptions() {
            goldenSpiralStarCount = optionsAccessor.GetValueInt32("GoldenSpiralStarCount", 9);
            siderealTrackStartOffsetSeconds = optionsAccessor.GetValueInt32("SiderealTrackStartOffsetSeconds", 0);
            siderealTrackEndOffsetSeconds = optionsAccessor.GetValueInt32("SiderealTrackEndOffsetSeconds", 0);
            siderealTrackRADeltaDegrees = optionsAccessor.GetValueDouble("SiderealTrackRADeltaDegrees", 1.5d);
            domeShutterWidth_mm = optionsAccessor.GetValueInt32("DomeShutterWidth_mm", 0);
            minimizeDomeMovementEnabled = optionsAccessor.GetValueBoolean("MinimizeDomeMovementEnabled", true);
            modelPointGenerationType = optionsAccessor.GetValueEnum("ModelPointGenerationType", ModelPointGenerationTypeEnum.GoldenSpiral);
            builderNumRetries = optionsAccessor.GetValueInt32("BuilderNumRetries", 0);
            westToEastSorting = optionsAccessor.GetValueBoolean("WestToEastSorting", false);
            maxPointRMS = optionsAccessor.GetValueDouble("MaxPointRMS", double.NaN);
            logCommands = optionsAccessor.GetValueBoolean("LogCommands", false);
            maxConcurrency = optionsAccessor.GetValueInt32("MaxConcurrency", 3);
            allowBlindSolves = optionsAccessor.GetValueBoolean("AllowBlindSolves", false);
            syncFirstPoint = optionsAccessor.GetValueBoolean("SyncFirstPoint", true);
            minPointAltitude = optionsAccessor.GetValueInt32("MinPointAltitude", 0);
            maxPointAltitude = optionsAccessor.GetValueInt32("MaxPointAltitude", 90);
            showRemovedPoints = optionsAccessor.GetValueBoolean("ShowRemovedPoints", true);
        }

        public void ResetDefaults() {
            GoldenSpiralStarCount = 9;
            SiderealTrackStartOffsetSeconds = 0;
            SiderealTrackEndOffsetSeconds = 0;
            SiderealTrackRADeltaDegrees = 1.5d;
            DomeShutterWidth_mm = 0;
            MinimizeDomeMovementEnabled = true;
            ModelPointGenerationType = ModelPointGenerationTypeEnum.GoldenSpiral;
            BuilderNumRetries = 0;
            WestToEastSorting = false;
            MaxPointRMS = double.NaN;
            LogCommands = false;
            MaxConcurrency = 3;
            AllowBlindSolves = false;
            SyncFirstPoint = true;
            MinPointAltitude = 0;
            MaxPointAltitude = 90;
            ShowRemovedPoints = true;
        }

        private int minPointAltitude;

        public int MinPointAltitude {
            get => minPointAltitude;
            set {
                if (minPointAltitude != value) {
                    if (value < 0 || value > 90) {
                        throw new ArgumentException("MinPointAltitude must be between 0 and 90, inclusive", "MinPointAltitude");
                    }
                    minPointAltitude = value;
                    optionsAccessor.SetValueInt32("MinPointAltitude", minPointAltitude);
                    RaisePropertyChanged();
                }
            }
        }

        private int maxPointAltitude;

        public int MaxPointAltitude {
            get => maxPointAltitude;
            set {
                if (maxPointAltitude != value) {
                    if (value < 0 || value > 90) {
                        throw new ArgumentException("MaxPointAltitude must be between 0 and 90, inclusive", "MaxPointAltitude");
                    }
                    maxPointAltitude = value;
                    optionsAccessor.SetValueInt32("MaxPointAltitude", maxPointAltitude);
                    RaisePropertyChanged();
                }
            }
        }

        private int goldenSpiralStarCount;

        public int GoldenSpiralStarCount {
            get => goldenSpiralStarCount;
            set {
                if (goldenSpiralStarCount != value) {
                    if (value < 3 || value > 90) {
                        throw new ArgumentException("GoldenSpiralStarCount must be between 3 and 90, inclusive", "GoldenSpiralStarCount");
                    }
                    goldenSpiralStarCount = value;
                    optionsAccessor.SetValueInt32("GoldenSpiralStarCount", goldenSpiralStarCount);
                    RaisePropertyChanged();
                }
            }
        }

        private int siderealTrackStartOffsetSeconds;

        public int SiderealTrackStartOffsetSeconds {
            get => siderealTrackStartOffsetSeconds;
            set {
                if (siderealTrackStartOffsetSeconds != value) {
                    siderealTrackStartOffsetSeconds = value;
                    optionsAccessor.SetValueInt32("SiderealTrackStartOffsetSeconds", siderealTrackStartOffsetSeconds);
                    RaisePropertyChanged();
                }
            }
        }

        private int siderealTrackEndOffsetSeconds;

        public int SiderealTrackEndOffsetSeconds {
            get => siderealTrackEndOffsetSeconds;
            set {
                if (siderealTrackEndOffsetSeconds != value) {
                    siderealTrackEndOffsetSeconds = value;
                    optionsAccessor.SetValueInt32("SiderealTrackEndOffsetSeconds", siderealTrackEndOffsetSeconds);
                    RaisePropertyChanged();
                }
            }
        }

        private double siderealTrackRADeltaDegrees;

        public double SiderealTrackRADeltaDegrees {
            get => siderealTrackRADeltaDegrees;
            set {
                if (siderealTrackRADeltaDegrees != value) {
                    if (value <= 0.0d) {
                        throw new ArgumentException("SiderealTrackRADeltaDegrees must be positive", "SiderealTrackRADeltaDegrees");
                    }
                    siderealTrackRADeltaDegrees = value;
                    optionsAccessor.SetValueDouble("SiderealTrackRADeltaDegrees", siderealTrackRADeltaDegrees);
                    RaisePropertyChanged();
                }
            }
        }

        private int domeShutterWidth_mm;

        public int DomeShutterWidth_mm {
            get => domeShutterWidth_mm;
            set {
                if (domeShutterWidth_mm != value) {
                    if (value < 0) {
                        throw new ArgumentException("DomeShutterWidth_mm must be non-negative", "DomeShutterWidth_mm");
                    }
                    domeShutterWidth_mm = value;
                    optionsAccessor.SetValueInt32("DomeShutterWidth_mm", domeShutterWidth_mm);
                    RaisePropertyChanged();
                }
            }
        }

        private bool minimizeDomeMovementEnabled;

        public bool MinimizeDomeMovementEnabled {
            get => minimizeDomeMovementEnabled;
            set {
                if (minimizeDomeMovementEnabled != value) {
                    minimizeDomeMovementEnabled = value;
                    optionsAccessor.SetValueBoolean("MinimizeDomeMovementEnabled", minimizeDomeMovementEnabled);
                    RaisePropertyChanged();
                }
            }
        }

        private ModelPointGenerationTypeEnum modelPointGenerationType;

        public ModelPointGenerationTypeEnum ModelPointGenerationType {
            get => modelPointGenerationType;
            set {
                if (modelPointGenerationType != value) {
                    modelPointGenerationType = value;
                    optionsAccessor.SetValueEnum("ModelPointGenerationType", modelPointGenerationType);
                    RaisePropertyChanged();
                }
            }
        }

        private bool westToEastSorting;

        public bool WestToEastSorting {
            get => westToEastSorting;
            set {
                if (westToEastSorting != value) {
                    westToEastSorting = value;
                    optionsAccessor.SetValueBoolean("WestToEastSorting", westToEastSorting);
                    RaisePropertyChanged();
                }
            }
        }

        private int builderNumRetries;

        public int BuilderNumRetries {
            get => builderNumRetries;
            set {
                if (builderNumRetries != value) {
                    if (value < 0) {
                        throw new ArgumentException("BuilderNumRetries must be non-negative", "BuilderNumRetries");
                    }
                    builderNumRetries = value;
                    optionsAccessor.SetValueInt32("BuilderNumRetries", builderNumRetries);
                    RaisePropertyChanged();
                }
            }
        }

        private double maxPointRMS;

        public double MaxPointRMS {
            get => maxPointRMS;
            set {
                if (maxPointRMS != value) {
                    if (value <= 0.0d || double.IsNaN(value)) {
                        maxPointRMS = double.NaN;
                    } else {
                        maxPointRMS = value;
                    }
                    optionsAccessor.SetValueDouble("MaxPointRMS", maxPointRMS);
                    RaisePropertyChanged();
                }
            }
        }

        private bool logCommands;

        public bool LogCommands {
            get => logCommands;
            set {
                if (logCommands != value) {
                    logCommands = value;
                    optionsAccessor.SetValueBoolean("LogCommands", logCommands);
                    RaisePropertyChanged();
                }
            }
        }

        private bool syncFirstPoint;

        public bool SyncFirstPoint {
            get => syncFirstPoint;
            set {
                if (syncFirstPoint != value) {
                    syncFirstPoint = value;
                    optionsAccessor.SetValueBoolean("SyncFirstPoint", syncFirstPoint);
                    RaisePropertyChanged();
                }
            }
        }

        private bool allowBlindSolves;

        public bool AllowBlindSolves {
            get => allowBlindSolves;
            set {
                if (allowBlindSolves != value) {
                    allowBlindSolves = value;
                    optionsAccessor.SetValueBoolean("AllowBlindSolves", allowBlindSolves);
                    RaisePropertyChanged();
                }
            }
        }

        private int maxConcurrency;

        public int MaxConcurrency {
            get => maxConcurrency;
            set {
                if (maxConcurrency != value) {
                    if (maxConcurrency < 0) {
                        throw new ArgumentException("MaxConcurrency must be non-negative", "MaxConcurrency");
                    }
                    maxConcurrency = value;
                    optionsAccessor.SetValueInt32("MaxConcurrency", maxConcurrency);
                    RaisePropertyChanged();
                }
            }
        }

        private bool showRemovedPoints;

        public bool ShowRemovedPoints {
            get => showRemovedPoints;
            set {
                if (showRemovedPoints != value) {
                    showRemovedPoints = value;
                    optionsAccessor.SetValueBoolean("ShowRemovedPoints", showRemovedPoints);
                    RaisePropertyChanged();
                }
            }
        }
    }
}