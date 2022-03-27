using SBCore.Configurator.TextResources;
using System;
using System.Collections.Generic;

namespace SBCore.Configurator
{
    public class TableOptions
    {
        public bool UserFilled { get; set; }
        public Dictionary<string, Tuple<string, object>[]> TableMap { get; set; }
    }

    internal static class StaticResources
    {
        public const string ADMIN = "ADMIN";
        public const string PARKING_MAP = "PARKING_MAP";
        public const string AIS_LOG = "AIS_LOG";
        public const string AIS_REPLACEMENT = "AIS_REPLACEMENT";
        public const string CAMERA_MAP = "CAMERA_MAP";
        public const string CMIUGW_AISSESSION_DELIVERY_STATUS = "CMIUGW_AISSESSION_DELIVERY_STATUS";
        public const string CMIUGW_CASHSTATUS = "CMIUGW_CASHSTATUS";
        public const string CMIUGW_CONFIG = "CMIUGW_CONFIG";
        public const string CMIUGW_DEVICE_MAP = "CMIUGW_DEVICE_MAP";
        public const string CMIUGW_EVENTS = "CMIUGW_EVENTS";
        public const string CMIUGW_IMAGES = "CMIUGW_IMAGES";
        public const string CMIUGW_ISSIMAGES = "CMIUGW_ISSIMAGES";
        public const string CMIUGW_LEVELS = "CMIUGW_LEVELS";
        public const string CMIUGW_LPR = "CMIUGW_LPR";
        public const string CMIUGW_MOVEMENTS = "CMIUGW_MOVEMENTS";
        public const string CMIUGW_PARKING_LEVEL_MAP = "CMIUGW_PARKING_LEVEL_MAP";
        public const string CMIUGW_PAYMENTS = "CMIUGW_PAYMENTS";
        public const string CMIUGW_PLACES = "CMIUGW_PLACES";
        public const string CMIUGW_PLACES_URLS_MAP = "CMIUGW_PLACES_URLS_MAP";
        public const string DEVICE_CMIU_CAMERA_MAP = "DEVICE_CMIU_CAMERA_MAP";
        public const string EVENT_CAMERA_MAP = "EVENT_CAMERA_MAP";
        public const string EXEMPTION_JOBS = "EXEMPTION_JOBS";
        public const string EXEMPTION_LOG = "EXEMPTION_LOG";
        public const string EXEMPTION_PARKING = "EXEMPTION_PARKING";
        public const string EXEMPTION_PROVIDERS = "EXEMPTION_PROVIDERS";
        public const string LPR_ISS_EVENT_MAP = "LPR_ISS_EVENT_MAP";

        public const string IX_AIS_LOG_Card = "IX_AIS_LOG_Card";
        public const string IX_AIS_LOG_OperationNumber = "IX_AIS_LOG_OperationNumber";
        public const string IX_AIS_LOG_SessionId = "IX_AIS_LOG_SessionId";
        public const string IX_AIS_LOG_StartTime = "IX_AIS_LOG_StartTime";
        public const string IX_AIS_REPLACEMENT_ReplacedCard = "IX_AIS_REPLACEMENT_ReplacedCard";
        public const string IX_DEVICE_CMIU_CAMERA_MAP_CameraId = "IX_DEVICE_CMIU_CAMERA_MAP_CameraId";
        public const string IX_DEVICE_CMIU_CAMERA_MAP_DeviceId = "IX_DEVICE_CMIU_CAMERA_MAP_DeviceId";
        public const string IX_EVENT_CAMERA_MAP_DeviceCmiuCameraId = "IX_EVENT_CAMERA_MAP_DeviceCmiuCameraId";
        public const string IX_EXEMPTION_JOBS_EntryTime = "IX_EXEMPTION_JOBS_EntryTime";
        public const string IX_EXEMPTION_LOG_EntryTime = "IX_EXEMPTION_LOG_EntryTime";
        public const string IX_LPR_ISS_EVENT_MAP_DeviceCmiuCameraId = "IX_LPR_ISS_EVENT_MAP_DeviceCmiuCameraId";

        public static string CreateScript(string table)
        {
            var createScript = table switch
            {
                ADMIN => SQLite.SqlCreate_ADMIN,
                PARKING_MAP => SQLite.SqlCreate_PARKING_MAP,
                AIS_LOG => SQLite.SqlCreate_AIS_LOG,
                AIS_REPLACEMENT => SQLite.SqlCreate_AIS_REPLACEMENT,
                CAMERA_MAP => SQLite.SqlCreate_CAMERA_MAP,
                CMIUGW_AISSESSION_DELIVERY_STATUS => SQLite.SqlCreate_CMIUGW_AISSESSION_DELIVERY_STATUS,
                CMIUGW_CASHSTATUS => SQLite.SqlCreate_CMIUGW_CASHSTATUS,
                CMIUGW_CONFIG => SQLite.SqlCreate_CMIUGW_CONFIG,
                CMIUGW_DEVICE_MAP => SQLite.SqlCreate_CMIUGW_DEVICE_MAP,
                CMIUGW_EVENTS => SQLite.SqlCreate_CMIUGW_EVENTS,
                CMIUGW_IMAGES => SQLite.SqlCreate_CMIUGW_IMAGES,
                CMIUGW_ISSIMAGES => SQLite.SqlCreate_CMIUGW_ISSIMAGES,
                CMIUGW_LEVELS => SQLite.SqlCreate_CMIUGW_LEVELS,
                CMIUGW_LPR => SQLite.SqlCreate_CMIUGW_LPR,
                CMIUGW_MOVEMENTS => SQLite.SqlCreate_CMIUGW_MOVEMENTS,
                CMIUGW_PARKING_LEVEL_MAP => SQLite.SqlCreate_CMIUGW_PARKING_LEVEL_MAP,
                CMIUGW_PAYMENTS => SQLite.SqlCreate_CMIUGW_PAYMENTS,
                CMIUGW_PLACES => SQLite.SqlCreate_CMIUGW_PLACES,
                CMIUGW_PLACES_URLS_MAP => SQLite.SqlCreate_CMIUGW_PLACES_URLS_MAP,
                DEVICE_CMIU_CAMERA_MAP => SQLite.SqlCreate_DEVICE_CMIU_CAMERA_MAP,
                EVENT_CAMERA_MAP => SQLite.SqlCreate_EVENT_CAMERA_MAP,
                EXEMPTION_JOBS => SQLite.SqlCreate_EXEMPTION_JOBS,
                EXEMPTION_LOG => SQLite.SqlCreate_EXEMPTION_LOG,
                EXEMPTION_PARKING => SQLite.SqlCreate_EXEMPTION_PARKING,
                EXEMPTION_PROVIDERS => SQLite.SqlCreate_EXEMPTION_PROVIDERS,
                LPR_ISS_EVENT_MAP => SQLite.SqlCreate_LPR_ISS_EVENT_MAP,
                _ => null,
            };
            return createScript;
        }

        public static Dictionary<string, Tuple<string, string>[]> SBCoreIndexMap { get; } = new()
        {
            {
                AIS_LOG,
                new Tuple<string, string>[]
                {
                    new(IX_AIS_LOG_Card, SQLite.SqlCreate_IX_AIS_LOG_Card),
                    new(IX_AIS_LOG_OperationNumber, SQLite.SqlCreate_IX_AIS_LOG_OperationNumber),
                    new(IX_AIS_LOG_SessionId, SQLite.SqlCreate_IX_AIS_LOG_SessionId),
                    new(IX_AIS_LOG_StartTime, SQLite.SqlCreate_IX_AIS_LOG_StartTime),
                }
            },
            {
                AIS_REPLACEMENT,
                new Tuple<string, string>[]
                {
                    new(IX_AIS_REPLACEMENT_ReplacedCard, SQLite.SqlCreate_IX_AIS_REPLACEMENT_ReplacedCard),
                }
            },
            {
                DEVICE_CMIU_CAMERA_MAP,
                new Tuple<string, string>[]
                {
                    new(IX_DEVICE_CMIU_CAMERA_MAP_CameraId, SQLite.SqlCreate_IX_DEVICE_CMIU_CAMERA_MAP_CameraId),
                    new(IX_DEVICE_CMIU_CAMERA_MAP_DeviceId, SQLite.SqlCreate_IX_DEVICE_CMIU_CAMERA_MAP_DeviceId),
                }
            },
            {
                EVENT_CAMERA_MAP,
                new Tuple<string, string>[]
                {
                    new(IX_EVENT_CAMERA_MAP_DeviceCmiuCameraId, SQLite.SqlCreate_IX_EVENT_CAMERA_MAP_DeviceCmiuCameraId),
                }
            },
            {
                EXEMPTION_JOBS,
                new Tuple<string, string>[]
                {
                    new(IX_EXEMPTION_JOBS_EntryTime, SQLite.SqlCreate_IX_EXEMPTION_JOBS_EntryTime),
                }
            },
            {
                EXEMPTION_LOG,
                new Tuple<string, string>[]
                {
                    new(IX_EXEMPTION_LOG_EntryTime, SQLite.SqlCreate_IX_EXEMPTION_LOG_EntryTime),
                }
            },
            {
                LPR_ISS_EVENT_MAP,
                new Tuple<string, string>[]
                {
                    new(IX_LPR_ISS_EVENT_MAP_DeviceCmiuCameraId, SQLite.SqlCreate_IX_LPR_ISS_EVENT_MAP_DeviceCmiuCameraId),
                }
            },
        };

        public static Dictionary<string, TableOptions> SBCoreTableMap { get; } = new()
        {
            {
                ADMIN,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Auth",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Expire",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                PARKING_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CellComputerNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FacilityNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Protocol",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ParkId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FrontId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Address",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                AIS_LOG,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Parking",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "SessionId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Operation",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Card",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "OperationNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Data",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "LPR",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "StartTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "StopTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FineCost",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FinePaid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AutoType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "SessionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "SubscriptionTicket",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Paid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExternalReference",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AnticipatedStopTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "PaymentTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "PaymentType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "RRN",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "NotDeliveredSum",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "NotDeliveredSumType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Exemption",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExemptionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeliveryStatus",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeliveryTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeliverAttempt",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                AIS_REPLACEMENT,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "NewCard",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ReplacedCard",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ProductionDate",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CAMERA_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Cameratype",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CameraUri",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Auth",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Compression",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Resolution",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Delay",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Timeout",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_AISSESSION_DELIVERY_STATUS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "SessionId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Card",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeliveryStatus",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeliveryTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_CASHSTATUS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "MoneyValue",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Quantity",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuMoneyType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuMoneyMethodType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_CONFIG,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Parameter",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Value",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_DEVICE_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CellComputerNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FacilityNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FieldDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeviceIP",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Protocol",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Port",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeviceAuth",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Prompt",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Reboot",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_EVENTS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuStatusNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuEventText",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ActionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_IMAGES,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Headers",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Data",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "BLOB"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_ISSIMAGES,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuCameraNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "IssCameraNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "HasLprRectangle",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "RectangleX",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "RectangleY",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "RectangleWidth",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "RectangleHeight",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ActionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_LEVELS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuLevelNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Free",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Busy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ActionTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_LPR,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "LprNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuCameraNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Traffic",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "RecognitionAccuracy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Speed",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ColorId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "CountryId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Direction",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ActionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_MOVEMENTS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "MovementType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuTransactionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Card",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuCardType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "LPR",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ActionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "EppSession",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "SessionId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Operation",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisSessionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisAutoType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FineCost",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FinePaid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_PARKING_LEVEL_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuLevelNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CounterNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_PAYMENTS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuDeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuPaymentMethod",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "PaymentMethodAddition",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Card",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuCardType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuPaymentType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuPaymentCount",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Price",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ActionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TransactionUid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DateEvent",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "EppSession",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "SessionId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Operation",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisSessionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisAutoType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisPaymentType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "AisExemptionType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FineCost",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "FinePaid",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_PLACES,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TotalFree",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "TotalBusy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ClientFree",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ClientBusy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ReservedFree",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ReservedBusy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "InvalidFree",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "InvalidBusy",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ActionTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                CMIUGW_PLACES_URLS_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuParkingNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Url",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Protocol",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                DEVICE_CMIU_CAMERA_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeviceId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "CmiuCameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "IssArchiveCameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "UsePfr",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                EVENT_CAMERA_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeviceCmiuCameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "EventId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                EXEMPTION_JOBS,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "JobId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "JobType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FacilityNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ComputerNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "OperatorNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeviceType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "DeviceNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ProviderId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "MediaType",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "MediaName10",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "MediaName16",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "EPAN",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "Troyka",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "EntryTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "LeaveLoopIn",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "LeaveLoopOut",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "OriginalAmount",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "RemainingAmount",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "REAL"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "LPR",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExemptionTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExemptionResult",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExemptionComment",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "TransactionTimeStamp",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "JobStatus",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "JobStatusTimeStamp",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                EXEMPTION_LOG,
                new TableOptions
                {
                    UserFilled = false,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Card",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "EntryTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "EntryDevice",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FacilityId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ParkingId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "Session",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "LPR",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "MetroCardId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ExemptionTime",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                        {
                            "ExemptionResult",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ExemptionComment",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                EXEMPTION_PARKING,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "FacilityId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ComputerNumber",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "MetroParkingId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
            {
                EXEMPTION_PROVIDERS,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ProviderId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "ProviderName",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", true)
                            }
                        },
                    }
                }
            },
            {
                LPR_ISS_EVENT_MAP,
                new TableOptions
                {
                    UserFilled = true,
                    TableMap = new Dictionary<string, Tuple<string, object>[]>
                    {
                        {
                            "Id",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("IsKey", true),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "DeviceCmiuCameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "TEXT"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "IssArchiveCameraId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                        {
                            "IssLprRecognizerId",
                            new Tuple<string, object>[]
                            {
                                new("DataTypeName", "INTEGER"),
                                new("AllowDBNull", false)
                            }
                        },
                    }
                }
            },
        };

        public static Dictionary<uint, string> EventMap { get; } = new()
        {
            { 2, "Event 0x0002/2" },
            { 3, "Event 0x0003/3" },
            { 4, "Event 0x0004/4" },
            { 2048, "Internal error" },
            { 2052, "Record not found" },
            { 4096, "Card movements record booking error PGL" },
            { 4097, "Settlement: Booking error!" },
            { 4102, "Cannot book with VSPROZ" },
            { 4105, "PA: Ringfile full!" },
            { 4106, "PA: Ringfile OK!" },
            { 4352, "No shift open for storing sales block" },
            { 4353, "Cannot create shift" },
            { 4354, "Cannot open shift" },
            { 4355, "Shift could not be reopened" },
            { 4356, "Event 0x1104/4356" },
            { 4357, "Cannot close shift" },
            { 4358, "Event 0x1106/4358" },
            { 4609, "Shift created" },
            { 4610, "Shift opened" },
            { 4611, "Shift reopened" },
            { 4612, "Shift closed" },
            { 4613, "Shift closed" },
            { 4614, "Event 0x1206/4614" },
            { 4882, "Error booking data from reservation service!" },
            { 7168, "Too many open shifts for user, device, computer, number:" },
            { 7169, "Open shift is expired" },
            { 7170, "DB resource problem detected!" },
            { 11519, "DB error during invoicing" },
            { 14337, "RAS connection error" },
            { 14338, "FTP connection error" },
            { 14339, "FTP file error" },
            { 14340, "File transfer is not working" },
            { 15360, "Data server: Error at data routing" },
            { 15361, "Data server has detected a database problem" },
            { 16000, "Configuration of data replication has been changed." },
            { 16001, "Configuration of data replication is incomplete" },
            { 16002, "Data replication is reporting a problem with the target database" },
            { 16003, "Data replication is reporting a problem with the target database" },
            { 16004, "DBR: Connected to remote database" },
            { 16005, "Data replication has no connection to the target database" },
            { 16006, "Data replication starts the compression of the job stack" },
            { 16007, "Data replication reports an overflow of the job stack" },
            { 16008, "Data replication reports an overflow of the job stack" },
            { 18449, "Call accepted" },
            { 18450, "Call hung up" },
            { 20480, "Report could not be created" },
            { 28678, "Extended error code 06 SL20 (Basis 7006)" },
            { 28687, "Extended error code 0F SL20 (Basis 700F)" },
            { 28689, "Extended error code 11 SL20 (Basis 7011)" },
            { 28690, "Extended error code 12 SL20 (Basis 7012)" },
            { 28702, "Extended error code 1E SL20 (Basis 701E)" },
            { 28715, "Extended error code 2B SL20 (Basis 702B)" },
            { 28721, "Extended error code 31 SL20 (Basis 7031)" },
            { 28735, "Extended error code 3F SL20 (Basis 703F)" },
            { 28749, "Extended error code 4D SL20 (Basis 704D)" },
            { 28750, "Extended error code 4E SL20 (Basis 704E)" },
            { 28751, "Extended error code 4F SL20 (Basis 704F)" },
            { 28753, "Extended error code 51 SL20 (Basis 7051)" },
            { 28754, "Extended error code 52 SL20 (Basis 7052)" },
            { 28757, "Extended error code 55 SL20 (Basis 7055)" },
            { 28763, "Extended error code 5B SL20 (Basis 705B)" },
            { 28764, "Extended error code 5C SL20 (Basis 705C)" },
            { 28765, "Extended error code 5D SL20 (Basis 705D)" },
            { 28766, "Extended error code 5E SL20 (Basis 705E)" },
            { 28785, "Extended error code 71 SL20 (Basis 7071)" },
            { 29696, "Connect to host failed" },
            { 29698, "Transaction cancelled" },
            { 29702, "Uncertain payment" },
            { 29704, "Card expired" },
            { 29705, "Card Reading Error" },
            { 29706, "EPT ready for payment" },
            { 29707, "Payment terminal is not ready for operations" },
            { 29708, "Card payment processing status is not clear" },
            { 29713, "Payment terminal requires a settlement closing" },
            { 29769, "Connection to terminal failed" },
            { 29770, "Transaction cancelled" },
            { 29771, "Transaction cancelled by the terminal" },
            { 29772, "Transaction cancelled by the user" },
            { 29773, "Transaction cancelled by the bank" },
            { 29774, "Card reading error" },
            { 29775, "Card expired" },
            { 29776, "Uncertain payment" },
            { 29934, "FiscalBox: The reprint operation has failed" },
            { 29935, "FiscalBox: Error in conversion to ELOT-928 Cr and Lf violation" },
            { 29936, "FiscalBox: Sign verification error" },
            { 29937, "FiscalBox: Missing A files in recovery" },
            { 29938, "FiscalBox: Last Z checking failed" },
            { 29939, "FiscalBox: General path/file error" },
            { 29940, "FiscalBox: No files to re-sign" },
            { 29941, "FiscalBox: General re-sign error" },
            { 29942, "FiscalBox: Error in re-loading last day sign files" },
            { 29943, "FiscalBox: Ini file error in create of Z files" },
            { 29944, "FiscalBox: Ini file error in create a and b files" },
            { 29945, "FiscalBox: Error in conversion to ELOT-928" },
            { 29946, "FiscalBox: General file to sign error" },
            { 29947, "FiscalBox: Cannot communicate with the box. Number of retries (3) has been exceeded." },
            { 29948, "FiscalBox: Cannot get a reply from the box" },
            { 29949, "FiscalBox: Cannot poll (find) the box" },
            { 29950, "FiscalBox: Cannot setup specified Com port" },
            { 29951, "FiscalBox: Cannot open specified Com port" },
            { 29952, "FiscalBox: OK - no error" },
            { 29953, "FiscalBox: Wrong Number of fields" },
            { 29954, "FiscalBox: Large field" },
            { 29955, "FiscalBox: Small field" },
            { 29956, "FiscalBox: Wrong fields length" },
            { 29957, "FiscalBox: Wrong fields" },
            { 29958, "FiscalBox: Wrong command - error command" },
            { 29961, "FiscalBox: Wrong printing type" },
            { 29962, "FiscalBox: Day active. Print Z report" },
            { 29963, "FiscalBox: Contact service!" },
            { 29964, "FiscalBox: Wrong date or time" },
            { 29965, "FiscalBox: No transactions at this period" },
            { 29967, "FiscalBox: No other word changes" },
            { 29968, "FiscalBox: Day active" },
            { 29969, "FiscalBox: Closed BLOCK" },
            { 29970, "FiscalBox: Wrong transactions - Data error" },
            { 29971, "FiscalBox: Wrong signature" },
            { 29972, "FiscalBox: Print Z report. 24 hours have passed since the last report." },
            { 29973, "FiscalBox: Wrong Z number. Z missing" },
            { 29974, "FiscalBox: Wrong Z transaction. Contact service!" },
            { 29975, "FiscalBox: User mode. Stop user mode" },
            { 29976, "FiscalBox: Signatures per day limit reached-exhausted. Print Z" },
            { 29977, "FiscalBox: End of paper" },
            { 29978, "FiscalBox: Disconnect printer" },
            { 29979, "FiscalBox: Disconnect fiscal memory" },
            { 29980, "FiscalBox: Fiscal box error. Contact service!" },
            { 29981, "FiscalBox: Fiscal memory full" },
            { 29982, "FiscalBox: No signature. Data missing." },
            { 29983, "FiscalBox: No signature" },
            { 29984, "FiscalBox: Low CMOS battery. Contact service!" },
            { 29985, "FiscalBox: Wrong command. Data retrieval active." },
            { 29986, "FiscalBox: Wrong command. Data retrieval ONLY after CMOS reset." },
            { 29987, "FiscalBox: Tax signing CMOS error" },
            { 29988, "FiscalBox: 48 hours have passed since the last report. Check Taxation signing Time/Date and/or pc." },
            { 29989, "FiscalBox: Wrong char in document." },
            { 30048, "FiscalBox: False frame by EAFDSS" },
            { 30049, "FiscalBox: Action cancelled by user (only AlgoDriver.exe)" },
            { 30051, "FiscalBox: Wrong Box serial no." },
            { 32769, "Ident type not found" },
            { 32770, "Ident type profile not found" },
            { 32771, "No identification type for identification number" },
            { 32772, "No record found in database of permitted media (WLIST)" },
            { 32773, "Medium not readable" },
            { 32774, "Data structure of medium not readable" },
            { 32775, "No record found in database of foreign media (TEMPCUST)" },
            { 32776, "Error deleting temporary customer" },
            { 32777, "No record found in database of device-specific permitted media (IDENTPROF)" },
            { 32778, "Customer is already present in system" },
            { 32780, "Medium is not valid anymore" },
            { 32781, "Erroneous expiry date on the track" },
            { 32782, "Medium is not yet valid" },
            { 32783, "Erroneous date format on the track" },
            { 32784, "Medium only in online mode permitted" },
            { 32785, "Wrong luhn check" },
            { 32786, "Amount exceeds the limit" },
            { 32787, "Wrong article" },
            { 32788, "National card: Wrong country" },
            { 32789, "Medium is not permitted in this country" },
            { 32790, "Track 1 for S&B ISO not found" },
            { 32791, "Error saving temporary customer" },
            { 32792, "The max. amount of permitted payments per day is reached" },
            { 32793, "Medium is invalid" },
            { 32794, "Card swallowed" },
            { 32795, "Customer PIN invalid" },
            { 32796, "No further PIN entry for this customer" },
            { 32797, "Reader is busy" },
            { 32798, "No online connection to the FEP" },
            { 32799, "No credit possible" },
            { 32803, "Card expired" },
            { 32806, "Error updating temporary customer" },
            { 32807, "Amount smaller than minimum amount" },
            { 32808, "Manual input is not for a card" },
            { 32809, "Medium functionality is blocked (ident type)" },
            { 32810, "Whitelist error" },
            { 32811, "Blacklist error" },
            { 32812, "Key management error" },
            { 32813, "Hard disk error" },
            { 32814, "External reader error" },
            { 32815, "No dynamic identification type profile found" },
            { 32816, "Customer has used an old identification medium" },
            { 32817, "Customer is on the issuer blacklist" },
            { 32818, "Customer has been blocked by the administrator" },
            { 32819, "Company is blocked by the administrator" },
            { 32820, "Customer is blocked by system settings" },
            { 32821, "Backout medium has been used" },
            { 32822, "Medium has been blocked by Tracking module" },
            { 32823, "Customer blocked by group counting" },
            { 32824, "Customer blocked - no company limit" },
            { 32825, "Customer blocked - 3 wrong PIN entries" },
            { 32831, "Card is blocked for 24 hours" },
            { 32832, "Card swallowed after timeout" },
            { 32833, "No reference to identification profile" },
            { 32834, "Error: deleting database cadafi" },
            { 32835, "Time stamp not between 1990 - 2025" },
            { 32836, "Empty ticket feeder SL20" },
            { 32837, "Amended identification type status" },
            { 32838, "Printer error (Receipt)" },
            { 32839, "General card error (not allowed now)" },
            { 32848, "EPT_05 Bank code no. blocked" },
            { 32849, "EPT_06 Error" },
            { 32850, "EPT_12 Invalid transaction" },
            { 32851, "EPT_19 Re-enter transaction" },
            { 32852, "EPT_51 Limit exceed" },
            { 32853, "Ept_54" },
            { 32854, "EPT_55 Wrong PIN" },
            { 32855, "EPT_57 Transaction not permitted to card holder" },
            { 32856, "EPT_61 Card blocked" },
            { 32857, "EPT_75 Maximum PIN input exceeded" },
            { 32858, "EPT_76 Reserved" },
            { 32859, "EPT_77 Reserved" },
            { 32860, "EPT_91 No connection to the issuer" },
            { 32861, "EPT_HOST error dialogue 200 to the Host" },
            { 32862, "EPT transaction stopped by the customer" },
            { 32865, "Cancel transaction with timeout" },
            { 32880, "Cannot erase card" },
            { 32881, "Cannot encode any new card" },
            { 32882, "Second card is invalid" },
            { 32883, "Customer is not in the database" },
            { 32884, "Card distributed" },
            { 32885, "Card is not distributed" },
            { 32886, "Card is not in the database" },
            { 32887, "Data error" },
            { 32888, "Malfunction of card distribution" },
            { 32896, "ERR more than 3 times blacklisted" },
            { 32897, "ERR error: read file header" },
            { 32898, "ERR error: write file header" },
            { 32899, "Customer to be deleted is not in blacklist" },
            { 32900, "Error deleting an old blacklist" },
            { 32901, "Error saving blacklist entry" },
            { 32902, "Error updating blacklist entry" },
            { 32903, "Error deleting blacklist entry" },
            { 32904, "Error checking temporary customer against blacklist" },
            { 32905, "Error updating temporary customer" },
            { 32906, "Error updating wild carding parameter WL/BL" },
            { 32907, "Error deleting temporary customer at 00:00" },
            { 32908, "Error updating identification type profile" },
            { 32909, "Application tries to change blacklist entry of the issuer" },
            { 32910, "Application tries to delete blacklist entry of the issuer" },
            { 32911, "Database connection problem reported" },
            { 32912, "Timeout during event processing" },
            { 32913, "Command could not be processed" },
            { 32914, "Cancellation by user" },
            { 32915, "Card reader not ready" },
            { 32916, "MCL: Reserve 1" },
            { 32917, "MCL: Reserve 2" },
            { 32918, "MCL: Reserve 3" },
            { 32919, "MCL: Reserve 4" },
            { 32920, "MCL: Reserve 5" },
            { 32921, "MCL: Reserve 6" },
            { 32922, "MCL: Reserve 7" },
            { 32923, "MCL: Reserve 8" },
            { 32924, "MCL: Reserve 9" },
            { 32959, "Unprocessed electronic payments have been found" },
            { 32973, "Ident profile entry not found" },
            { 32987, "Unprocessed electronic payment files have been found" },
            { 32988, "Credit card system reports an accounting problem (WA)" },
            { 32990, "Credit card system reports that accounting failed" },
            { 33025, "EC card not permitted" },
            { 33026, "Amount beyond the limit (EC card)" },
            { 33027, "Cancellation not possible (EC card)" },
            { 33028, "Invalid request (EC card)" },
            { 33029, "EC card expired" },
            { 33030, "EC card not accepted" },
            { 33031, "Wrong PIN" },
            { 33032, "EC card invalid" },
            { 33033, "Wrong EC card" },
            { 33034, "EC card rejected" },
            { 33035, "No EC cancellation executed" },
            { 33036, "Wrong PIN input too often" },
            { 33037, "System error 76 (EC card)" },
            { 33038, "System error 91 (EC card)" },
            { 33039, "System error 92 (EC card)" },
            { 33040, "System error 96 (EC card)" },
            { 33041, "EC MAC invalid" },
            { 33042, "EC date invalid" },
            { 33043, "System error 99 (EC card)" },
            { 33044, "MAC from the ST10 wrong - no PAC generated" },
            { 33045, "Wrong nty in the EC authorization response" },
            { 33046, "Wrong PAN in the EC authorization response" },
            { 33047, "Wrong Pnr in the EC authorization response" },
            { 33048, "Wrong Hgn in the EC authorization response" },
            { 33049, "Wrong trace number in the EC authorization response" },
            { 33050, "Wrong amount in the EC authorization response" },
            { 33051, "Wrong date / time in the EC authorization response" },
            { 33052, "Wrong station ID in der EC authorization response" },
            { 33053, "MAC from FEP wrong" },
            { 33054, "Response code from FEP not OK" },
            { 33057, "Currency key of EC card wrong" },
            { 33064, "Counter for \"Operating error by user\" of EC card wrong" },
            { 33101, "Chip error" },
            { 33280, "Control data record not found" },
            { 33281, "EPAN not found" },
            { 33282, "No card data field for this ident type" },
            { 33283, "Card data field record is missing" },
            { 33284, "Record not found" },
            { 33285, "Event in wrong phase" },
            { 33286, "Event cancellation (special user status code)" },
            { 33287, "Error KL process (rej. ti. with wrong EPAN)" },
            { 33288, "Issuer data not found" },
            { 33289, "Manual input incorrect" },
            { 33392, "Connection with terminal reader OK" },
            { 33393, "Communication error with terminal reader" },
            { 33394, "Customer not registered as season parker" },
            { 33536, "Error EKM file" },
            { 33537, "Error status from SL20" },
            { 33538, "Error status from EPT" },
            { 33539, "Error ident number system" },
            { 33540, "Setup error ident number system" },
            { 33541, "Initialization of the EC authorization not OK" },
            { 33542, "EC terminal is busy" },
            { 33543, "OAS process remote data transmission not OK" },
            { 33544, "Timeout message from SSD process" },
            { 33545, "Timeout remote data transmission dialogue" },
            { 33546, "System status remote data transmission response not OK" },
            { 33547, "System state OAS process response not OK" },
            { 33548, "Encrypted key for pin calculation not found" },
            { 33549, "Key file not found" },
            { 33550, "No response from blacklist check event etc." },
            { 33616, "Interpreter of identification numbers not ready" },
            { 33617, "Temporary customer process not ready" },
            { 33618, "Power failure temp. customer process" },
            { 33619, "Check process for ident numbers not ready" },
            { 33620, "Power failure at identification number check process" },
            { 33621, "Offset exp.date in ident check profile" },
            { 33622, "Offset eff.date in ident check profile" },
            { 33623, "Offset eff.date in ident check profile" },
            { 33624, "Error upon opening the identification database" },
            { 33625, "Power failure at identification number update process" },
            { 33626, "No expiry date found for blacklist check" },
            { 33627, "IAS for this terminal is in preparation" },
            { 33628, "Reader not in the reader buffer" },
            { 33629, "No response on query regarding blacklist or temporary customers possible" },
            { 33630, "TS not ready (AS->UI) (leh)" },
            { 33631, "Credit card system reports that blocking list is offline (IAS)" },
            { 33632, "Clean reader (KL process event 0x0025)" },
            { 33633, "Print test ticket 1" },
            { 33634, "Print test ticket 2" },
            { 33635, "Print test ticket 3" },
            { 33636, "Refill ticket stack 1" },
            { 33637, "Refill ticket stack 2" },
            { 33638, "Refill ticket stack 3" },
            { 33665, "PT_XML: wrong amount financial settlement" },
            { 33666, "PT_XML: financial settlement declined" },
            { 33669, "Error updating credit card authorization in database (TTRANSAUTH)" },
            { 33671, "PT_XML: lost connection at authorization" },
            { 33672, "PT_XML: lost connection at settlement" },
            { 33673, "PT_XML: error offline processing" },
            { 33674, "Credit card authorization: unknown offline result" },
            { 33675, "PT_XML: find request to double authorisation" },
            { 33676, "Credit card clearing: Payware password expiration" },
            { 33677, "Credit card system reports a proccesing error for offline transactions" },
            { 33678, "OAS: error processing offline transaction" },
            { 33679, "OAS: lost cc server connection" },
            { 33680, "OAS: error saving offline transaction" },
            { 33686, "Credit processing offline, loss of money possible" },
            { 33687, "Credit offline limit reached, no credit possible" },
            { 33696, "Service unavailable - antenna malfunction!" },
            { 33697, "No ViaVerde TAG" },
            { 33698, "ViaVerde TAG invalid" },
            { 33699, "ViaVerde TAG expired" },
            { 33700, "Client received transaction error from local server" },
            { 33701, "Client lost connection to local server" },
            { 33702, "ViaVerde - Antenna OK" },
            { 33703, "Server general error code" },
            { 33704, "Server is initializing" },
            { 33705, "Server open database connection error" },
            { 33706, "Server read/write Database operation error" },
            { 33707, "Server transaction parsing error" },
            { 33708, "Server open connection error with clearing server" },
            { 33709, "Server successfully opened connection to clearing server" },
            { 33710, "Server reading data sent from clearing server (socket error)" },
            { 33711, "Server sending data to clearing server (socket error)" },
            { 33712, "Server lost connection with clearing server" },
            { 33713, "Server succesfully restored lost connection with clearing server" },
            { 33714, "Server closed connection with clearing server" },
            { 33715, "Server terminated" },
            { 33716, "Server lost connection with an Entry/Exit device" },
            { 33717, "Server restored connection with an Entry/Exit device" },
            { 33728, "Transaction refused" },
            { 33729, "Unknown problem. No Validation." },
            { 33744, "EFT process for external EMV terminal started properly" },
            { 33745, "External EMV terminal ready for payment (sign on)" },
            { 33746, "External EMV terminal not ready for payment (sign off)" },
            { 33747, "Protocol error during transaction" },
            { 33748, "Internal process error" },
            { 33749, "Terminal/PSAM update" },
            { 33808, "Not used" },
            { 33809, "Slippage in paper switch (feed)" },
            { 33810, "Slippage in paper switch (issue)" },
            { 33811, "High voltage at receiver side of the paper switch" },
            { 33812, "HIGH level of LB cannot be achieved" },
            { 33813, "High voltage at receiver side of ticket feeder" },
            { 33814, "High voltage at receiver side" },
            { 33815, "HIGH level of LB cannot be achieved" },
            { 33816, "High voltage at receiver side" },
            { 33817, "HIGH level cannot be achieved" },
            { 33818, "High voltage at receiver side" },
            { 33819, "HIGH level of LB cannot be achieved" },
            { 33820, "HIGH level not within reach" },
            { 33824, "Erroneous cutter movement" },
            { 33825, "Slippage in ticket feeder (feed)" },
            { 33826, "Slippage in ticket feeder (issue)" },
            { 33827, "Stack 1: No paper" },
            { 33828, "Stack 2: No paper" },
            { 33829, "Stack 3: No paper" },
            { 33831, "Stack 5: No paper" },
            { 33832, "Stack 6: No paper" },
            { 33833, "Error in the initial paper position" },
            { 33834, "Card limit not found upon initialization" },
            { 33835, "Light barrier at cutter not calibratable" },
            { 33836, "Edge of the ticket not recognized" },
            { 33837, "Edge of the ticket not found" },
            { 33838, "Wrong ticket length" },
            { 33839, "Paper sensor faulty" },
            { 33840, "Voltage level not OK" },
            { 33841, "Slippage in the reader (feed)" },
            { 33842, "Slippage in the reader (issue)" },
            { 33843, "High voltage at receiver side of terminal 1" },
            { 33844, "Wrong ticket length" },
            { 33845, "HIGH level of LB cannot be achieved" },
            { 33846, "Not used" },
            { 33847, "Not used" },
            { 33848, "Reading error: No data recognized" },
            { 33849, "HIGH level of LB cannot be achieved" },
            { 33850, "Read-after-Write error" },
            { 33851, "High voltage at receiver side" },
            { 33852, "Voltage level not OK" },
            { 33853, "Magn. refer. not practicable" },
            { 33854, "No data on ticket" },
            { 33855, "Ticket jam in SL20" },
            { 33856, "Print head cannot be lifted" },
            { 33857, "Slippage in printer (feed)" },
            { 33858, "Slippage in printer (issue)" },
            { 33859, "Error generating print data" },
            { 33860, "Unknown command" },
            { 33861, "Too many characters per line" },
            { 33862, "Wrong command sequence" },
            { 33863, "Wrong command" },
            { 33864, "High voltage at receiver side" },
            { 33865, "HIGH level of LB cannot be achieved" },
            { 33866, "High voltage at receiver side" },
            { 33867, "HIGH level of LB cannot be achieved" },
            { 33868, "Printing position not refer." },
            { 33871, "Unknown error code of printer" },
            { 33872, "Ticket stack 1 OK" },
            { 33873, "Ticket stack 1 low" },
            { 33874, "Ticket stack 1 end" },
            { 33875, "Ticket stack 2 OK" },
            { 33876, "Ticket stack 2 low" },
            { 33877, "Ticket stack 2 end" },
            { 33878, "Ticket stack 3 OK" },
            { 33879, "Ticket stack 3 low" },
            { 33880, "Ticket stack 3 end" },
            { 33887, "Status code unknown" },
            { 33888, "Slippage in paper switch (feed)" },
            { 33889, "Slippage in paper switch (issue)" },
            { 33890, "Slippage in paper switch (collection box)" },
            { 33891, "Error activating paper switch" },
            { 33898, "EPT ready for payment" },
            { 33899, "Payment terminal is not ready for operations" },
            { 33900, "Card payment processing status is not clear" },
            { 33901, "Processing error at payment terminal occured" },
            { 33903, "Unknown error code paper switch" },
            { 33904, "SL20 OK - all previously reported errors repaired" },
            { 33911, "Unknown error code terminal" },
            { 33912, "SL20 NAK received v11/v24 line out of order" },
            { 33913, "SL20 in operation" },
            { 33920, "EPT nck received (line defective)" },
            { 33921, "EPT security module not ready (no response)" },
            { 33922, "EPT X.25 line down" },
            { 33923, "EPT Host protocol error" },
            { 33924, "EPT Error in shift server" },
            { 33935, "EPT ready" },
            { 33936, "No tickets in SL20" },
            { 33937, "Ticket in the paper switch" },
            { 33938, "Ticket in the reversing / torque unit" },
            { 33939, "Ticket in feeder 1" },
            { 33940, "Ticket in reader 1" },
            { 33941, "Reserve" },
            { 33942, "Ticket in feeder 2" },
            { 33943, "Ticket in printer position 2" },
            { 33944, "Ticket in reader 2" },
            { 33945, "Ticket in dispensing tray" },
            { 33946, "Ticket in collection box" },
            { 33947, "Reserve" },
            { 33948, "Reserve" },
            { 33949, "Reserve" },
            { 33950, "Reserve" },
            { 33951, "Reserve" },
            { 33952, "BKV: Uncritical terminal error" },
            { 33953, "BKV: Critical terminal error" },
            { 33954, "BKV: Uncritical printer error" },
            { 33955, "BKV: Critical printer error" },
            { 33956, "BKV: Uncritical PC30 error" },
            { 33957, "BKV: Critical PC30 error" },
            { 33958, "BKV: Uncritical paper switch error" },
            { 33959, "BKV: Critical paper switch error" },
            { 33960, "BKV: Record error" },
            { 33961, "BKV: Error Device out of order" },
            { 33962, "BKV: Error Device in order" },
            { 33963, "BKV: Dialog cannot be processed" },
            { 34047, "Unknown SL20 error code" },
            { 34049, "ST-SM not initialized" },
            { 34050, "No further attempt (PIN) permitted!!!" },
            { 34051, "No R_CUST_DATA found" },
            { 34052, "Timeout waiting for ST10 response" },
            { 34053, "User interruption upon PIN input" },
            { 34054, "Wrong response from ST10" },
            { 34055, "ST10 status not OK !" },
            { 34056, "Timeout upon FEP request" },
            { 34057, "PIN OK" },
            { 34058, "EC_CMD with error code or PIN incorrect" },
            { 34059, "INIT-CMD in queue" },
            { 34097, "Enable PinPASS" },
            { 34098, "Disable PinPASS" },
            { 34224, "Connection error" },
            { 34225, "Card not readable" },
            { 34226, "Wrong card data" },
            { 34227, "Card blocked" },
            { 34228, "File Error" },
            { 34296, "Remissio: Connection error" },
            { 34297, "Remissio: Protocol error" },
            { 34298, "Remissio: No memory" },
            { 34299, "Remissio: Not enough credit" },
            { 34300, "Remissio: Booking not OK" },
            { 34301, "Remissio: Could not exclude file" },
            { 34320, "Not used" },
            { 34321, "Slippage in paper switch (feed)" },
            { 34322, "Slippage in paper switch (issue)" },
            { 34323, "High voltage at receiver side of the paper switch" },
            { 34324, "HIGH level of LB cannot be achieved" },
            { 34325, "High voltage at receiver side of ticket feeder" },
            { 34326, "High voltage at receiver side" },
            { 34327, "HIGH level of LB cannot be achieved" },
            { 34328, "High voltage at receiver side" },
            { 34329, "HIGH level of fibre optics cannot be achieved" },
            { 34330, "High voltage at receiver side" },
            { 34331, "HIGH level of LB cannot be achieved" },
            { 34332, "HIGH level of fibre optics cannot be achieved" },
            { 34336, "Erroneous cutter movement" },
            { 34337, "Slippage in ticket feeder (feed)" },
            { 34338, "Slippage in ticket feeder (issue)" },
            { 34339, "Stack 1: No paper" },
            { 34340, "Stack 2: No paper" },
            { 34341, "Stack 3: No paper" },
            { 34343, "Stack 5: No paper" },
            { 34344, "Stack 6: No paper" },
            { 34345, "Error in the initial paper position" },
            { 34346, "Card limit not found upon initialization" },
            { 34347, "Light barrier at cutter not calibratable" },
            { 34348, "Edge of the ticket not recognized" },
            { 34349, "Edge of the ticket not found" },
            { 34350, "Wrong ticket length" },
            { 34351, "Paper sensor faulty" },
            { 34352, "Voltage level not OK" },
            { 34353, "Slippage in terminal (feed)" },
            { 34354, "Slippage in terminal (issue)" },
            { 34355, "High voltage at receiver side of terminal 1" },
            { 34356, "Wrong ticket length" },
            { 34357, "HIGH level of LB cannot be achieved" },
            { 34358, "Not used" },
            { 34359, "Not used" },
            { 34360, "Reading error: no data recognized" },
            { 34361, "HIGH level of LB cannot be achieved" },
            { 34362, "Read-after-Write error" },
            { 34363, "High voltage at receiver side" },
            { 34364, "Voltage level not OK" },
            { 34365, "Magnetic reference not practicable" },
            { 34366, "No data on ticket" },
            { 34367, "Ticket jam in SL20" },
            { 34368, "Print head cannot be lifted" },
            { 34369, "Slippage in printer (feed)" },
            { 34370, "Slippage in printer (issue)" },
            { 34371, "Error generating print data" },
            { 34372, "Unknown command" },
            { 34373, "Too many characters per line" },
            { 34374, "Wrong command sequence" },
            { 34375, "Wrong command" },
            { 34376, "High voltage at receiver side" },
            { 34377, "HIGH level of LB cannot be achieved" },
            { 34378, "High voltage at receiver side" },
            { 34379, "HIGH level of LB cannot be achieved" },
            { 34380, "Printing position not referable" },
            { 34383, "Unknown error code printer" },
            { 34384, "Ticket stack 1 OK" },
            { 34385, "Ticket stack 1 low" },
            { 34386, "Ticket stack 1 end" },
            { 34387, "Ticket stack 2 OK" },
            { 34388, "Ticket stack 2 low" },
            { 34389, "Ticket stack 2 end" },
            { 34390, "Ticket stack 3 OK" },
            { 34391, "Ticket stack 3 low" },
            { 34392, "Ticket stack 3 end" },
            { 34393, "Stack 1: Ticket with blank magstripe!" },
            { 34394, "Stack 2: Ticket with blank magstripe!" },
            { 34396, "Stack 1: Read-after-write error!" },
            { 34397, "Stack 2: Read-after-write error!" },
            { 34398, "Stack 1: Read-after-write OK!" },
            { 34399, "Status code unknown" },
            { 34400, "Slippage in paper switch (feed)" },
            { 34401, "Slippage in paper switch (issue)" },
            { 34402, "Slippage in paper switch (collection box)" },
            { 34403, "Error activating paper switch" },
            { 34415, "Unknown error code paper switch" },
            { 34416, "SL20 OK - all previously reported errors repaired" },
            { 34423, "Unknown error code terminal" },
            { 34424, "SL20 NAK received v11/v24 line out of order" },
            { 34425, "SL20 in operation" },
            { 34432, "EPT nack knowledge received (line defective)" },
            { 34433, "EPT security module not ready (no response)" },
            { 34434, "EPT X.25 line down" },
            { 34435, "EPT Host protocol error" },
            { 34436, "EPT error shift server" },
            { 34447, "EPT ready" },
            { 34448, "No tickets in SL20" },
            { 34449, "Ticket in paper switch" },
            { 34450, "Ticket in reversing / torque unit" },
            { 34451, "Ticket in feeder 1" },
            { 34452, "Ticket in terminal 1" },
            { 34453, "Reserve" },
            { 34454, "Ticket in feeder 2" },
            { 34455, "Ticket in printer position 2" },
            { 34456, "Ticket in terminal 2" },
            { 34457, "Ticket in dispensing tray" },
            { 34458, "Ticket in collection box" },
            { 34459, "Reserve" },
            { 34460, "Reserve" },
            { 34461, "Reserve" },
            { 34462, "Reserve" },
            { 34463, "Reserve" },
            { 34464, "Ticket not withdrawn from presenter" },
            { 34465, "Ticket not withdrawn from paper switch" },
            { 34466, "Ticket not distributed from presenter" },
            { 34467, "Ticket not distributed from paper switch" },
            { 34468, "Ticket not transp. from presenter to paper switch" },
            { 34469, "Ticket not transp. from tray to presenter" },
            { 34470, "Barcode warning not used" },
            { 34471, "Barcode warning not used" },
            { 34480, "BKV: Cutter cannot close" },
            { 34481, "BKV: Cutter cannot open" },
            { 34482, "BKV: Sensor 1 busy" },
            { 34483, "BKV: Sensor 2 busy" },
            { 34484, "BKV: Front sensor of print head busy" },
            { 34485, "BKV: Back sensor of print head busy" },
            { 34486, "BKV: Printer end sensor busy" },
            { 34487, "BKV: Sensor of paper switch busy" },
            { 34488, "BKV: Upper sensor of print head busy" },
            { 34489, "BKV: Lower sensor of print head busy" },
            { 34490, "BKV: Transport to basket problem" },
            { 34491, "BKV: Faulty terminal configuration" },
            { 34492, "BKV: Faulty paper switch configuration" },
            { 34493, "BKV: Faulty terminal configuration" },
            { 34494, "BKV: Faulty paper switch configuration" },
            { 34495, "BKV: Faulty printer/cutter configuration" },
            { 34496, "BKV: Faulty terminal + Prn/Cut configuration" },
            { 34497, "BKV: Faulty magnetic head configuration" },
            { 34498, "BKV: Magnet of paper switch w/o voltage" },
            { 34499, "BKV: Transport error (terminal -> front)" },
            { 34500, "BKV: Transport error (terminal -> front)" },
            { 34501, "BKV: Transport error (-> end position)" },
            { 34502, "BKV: Transport error (-> basket)" },
            { 34503, "BKV: Transport error (-> paper switch)" },
            { 34504, "BKV: Transport error (<- paper switch)" },
            { 34505, "BKV: Transport error (terminal -> front)" },
            { 34506, "BKV: No space between the tickets" },
            { 34533, "BKV: Wrong HEX record type" },
            { 34534, "BKV: Wrong HEX record format" },
            { 34535, "BKV: Wrong line CRC" },
            { 34536, "BKV: Unexpected EOF record" },
            { 34537, "BKV: Address out of bound" },
            { 34538, "BKV: Data packet timeout" },
            { 34539, "BKV: Flash write error" },
            { 34540, "BKV: Missing EOF record" },
            { 34544, "BKV: Input buffer pointer" },
            { 34545, "BKV: Input buffer length" },
            { 34546, "BKV: Production from empty stack" },
            { 34547, "BKV: Text element out of printable area" },
            { 34548, "BKV: Barcode element out of print. area" },
            { 34549, "BKV: Out of ticket borders settings" },
            { 34550, "BKV: Buffer allocation" },
            { 34551, "BKV: Unknown matrix" },
            { 34552, "BKV: No space to print a barcode" },
            { 34553, "BKV: Common production error" },
            { 34559, "Unknown SL20 error code" },
            { 34562, "Settlement system is reporting a booking error" },
            { 34563, "ARSS responds error ending shift" },
            { 34564, "ARSS responds error creating shift" },
            { 34567, "Booking buffer is OK" },
            { 34568, "Settlement system is reporting a full booking buffer" },
            { 34569, "Settlement system is reporting overflow of booking buffer" },
            { 34570, "Result of Abr FIFO Check (error accessing)" },
            { 34574, "Balance check" },
            { 34576, "Subsequent receipt: Invalid data" },
            { 34577, "Subsequent receipt: No data" },
            { 34578, "Subsequent receipt: No job" },
            { 34579, "Subsequent receipt: Timeout" },
            { 34580, "Subsequent receipt: System error" },
            { 34834, "Pointer does not exist" },
            { 34835, "CommListNo not permitted" },
            { 34836, "No data for grace time check" },
            { 34849, "Pointer does not exist" },
            { 34850, "Medium without access permission detected" },
            { 34851, "Medium with missing entry transaction detected" },
            { 34852, "Medium with cancelation flag detected" },
            { 34853, "Medium with exceeded grace time detected" },
            { 34854, "Device type not permitted" },
            { 34865, "Pointer not existing" },
            { 34866, "Medium is already present in the car park" },
            { 34867, "Medium with cancellation flag detected" },
            { 34868, "Medium out of valid access profile detected" },
            { 34869, "Medium with missing entry transaction detected" },
            { 34870, "Result wWpPayCal() : extra payment" },
            { 34871, "Fatal error in wWpPayCal()" },
            { 34872, "Device type not permitted" },
            { 34873, "Medium without access profile detected" },
            { 34874, "Card expired" },
            { 34881, "Pointer does not exist" },
            { 34882, "Medium with registered exit transaction (invalid) detected" },
            { 34883, "Medium is invalid due to a registered exit transaction" },
            { 34884, "Medium is used outside of the access profile" },
            { 34885, "Medium without associated entry transaction detected" },
            { 34886, "Medium is used outside of the access profile, extra payment is required" },
            { 34887, "Server error in wWpPayCal()" },
            { 34888, "Device type not permitted" },
            { 34889, "Medium has not been used yet" },
            { 34890, "Medium has already been used" },
            { 34891, "Fatal error in weekly programme valid() (return from wWpProfCheck)" },
            { 34892, "No money value on Parking Cheque" },
            { 34897, "Pointer does not exist" },
            { 34898, "Medium with registered entry transaction at entry detected" },
            { 34899, "Medium is invalid due to a registered exit transaction" },
            { 34900, "Medium is used outside of the access profile" },
            { 34901, "Medium without associated entry transaction detected" },
            { 34903, "Server error in wWpPayCal()" },
            { 34904, "Device type not permitted" },
            { 34906, "Medium is invalid for usage" },
            { 34907, "Medium is used outside of the access profile, extra payment is required" },
            { 34908, "Fatal error in WP_Valid() (return from wWpProfCheck)" },
            { 34913, "Pointer does not exist" },
            { 34914, "Device type not permitted" },
            { 34918, "Already present in unit" },
            { 34920, "Medium is used outside of the access profile" },
            { 34921, "Medium without associated entry transaction detected" },
            { 34922, "Extra payment is required" },
            { 34929, "Pointer does not exist" },
            { 34945, "Already present in unit" },
            { 34948, "Not present in unit" },
            { 34949, "Extra payment is required" },
            { 34951, "Device type not permitted" },
            { 34954, "Timeout reading card" },
            { 34976, "Tariff database cannot be opened" },
            { 34977, "Article database cannot be opened" },
            { 34978, "Error reading tariff database" },
            { 34981, "No tariff data found" },
            { 34982, "No validity data found" },
            { 34983, "No constant data found" },
            { 34984, "No step amount found (step amount tab)" },
            { 34985, "No tariff data reference found" },
            { 34986, "No tariff variable found" },
            { 34987, "No tariff step amount found" },
            { 34988, "No tol. minutes found" },
            { 34989, "No constant value found" },
            { 34990, "No tariff assignment found" },
            { 34991, "No entries in facility table found" },
            { 34992, "No card data field found" },
            { 34995, "No start time data found" },
            { 34998, "No scenario found or syntax error in scenario (ablauf.sze)" },
            { 35000, "No payment required" },
            { 35001, "Medium without associated access profile detected" },
            { 35008, "No magnetic ticket article found" },
            { 35009, "No base article found" },
            { 35010, "No article found" },
            { 35011, "No tariff article produced" },
            { 35024, "Maximum number of allowed free tariffs reached" },
            { 35025, "Time difference is <" },
            { 35040, "Insufficient memory buffer" },
            { 35056, "Tariff calculation error" },
            { 35078, "Pressing of special tariff was not accepted by CLIP" },
            { 35079, "Error re-encoding ticket" },
            { 35080, "Error producing receipt" },
            { 35081, "Error printing journal or booking error" },
            { 35087, "Replacement of value card failed" },
            { 35088, "Error cashback /emergency ticket" },
            { 35089, "Error printing receipt" },
            { 35090, "Error printing journal" },
            { 35092, "Check of change money failed" },
            { 35098, "Unsuccessful credit card usage!" },
            { 35101, "Ticket not removed in time" },
            { 35184, "Authorization error" },
            { 35185, "Authorization result" },
            { 35188, "Communication error" },
            { 35584, "LPR is online" },
            { 35585, "LPR is offline" },
            { 35586, "LPR: Capture error" },
            { 35587, "LPR: Database error" },
            { 35588, "LPR: License plate not found." },
            { 35589, "LPR: More than one license plate found." },
            { 35590, "LPR: Ticket and license plate are not equal." },
            { 35591, "LPR: Greylist: Insufficient funds." },
            { 35592, "LPR: Greylist: Lost Ticket." },
            { 35593, "LPR: Ticket not found." },
            { 35594, "LPR: More than one ticket found." },
            { 35595, "LPR: Exit not allowed." },
            { 35596, "LPR: Trigger missed." },
            { 35597, "LPR: License plate is on the greylist." },
            { 35600, "LPR: License plate is on the Lost Ticket List." },
            { 35601, "LPR: License plate is on the Open ISF List." },
            { 35602, "LPR: License plate is on the Paid ISF List." },
            { 35603, "LPR: License plate is on the Blacklist." },
            { 35604, "LPR: Wrong reservation for season parker." },
            { 35616, "Image Review: Multiple EPANs found." },
            { 35617, "Image Review: Multiple LPNs found." },
            { 35618, "Image Review: EPAN found, but low QF." },
            { 35619, "Image Review: EPAN not found." },
            { 35620, "Image Review: LPN not found." },
            { 35621, "Image Review: Operator notification." },
            { 35622, "Image Review: Lost Ticket with low QF." },
            { 35623, "Image Review: Duplicated entries." },
            { 35624, "Image Review: Low QF at entry." },
            { 35625, "Image Review: Greylist." },
            { 35626, "Image Review: LPN of entry and exit do not match." },
            { 35627, "Image Review: Swapped ticket." },
            { 35628, "Image Review: No trigger signal." },
            { 35629, "Image Review: No Whitelist entry." },
            { 35630, "Image Review: No EPAN and low QF." },
            { 35631, "Image Review: No LPN found." },
            { 35632, "Image Review: LPNs do not match." },
            { 35633, "Image Review: Low QF for credit card." },
            { 35634, "Image Review: Low QF for season parker." },
            { 35635, "Image Review: Low QF for season parker." },
            { 35636, "Image Review: Low QF at exit." },
            { 35637, "Image Review: Multiple EPANs found." },
            { 35638, "Image Review: Multiple LPNs found." },
            { 35639, "Image Review: EPAN found, but low QF." },
            { 35640, "Image Review: EPAN not found." },
            { 35641, "Image Review: LPN not found." },
            { 35642, "Image Review: Greylist." },
            { 35643, "Image Review: LPN of entry and exit do not match." },
            { 35664, "LPI: lpnx and lpnn do not match" },
            { 35744, "LPR: Lost Ticket function called." },
            { 35745, "LPR: Unreadable Ticket function called." },
            { 35746, "LPR: Towed Vehicle function called." },
            { 35747, "LPR: Get Back Towed Vehicle function called." },
            { 35748, "LPR: Rental Car function called." },
            { 35749, "LPR: Insufficient Fund function called." },
            { 35750, "LPR: Pay Insufficient Fund function called." },
            { 35751, "LPR: Delete Insufficient Fund function called." },
            { 35752, "LPR: Oversized Vehicle function called." },
            { 35840, "Journal printer: paper end" },
            { 35841, "Journal printer: paper low" },
            { 35842, "Journal printer: general I/O error" },
            { 35843, "Journal printer OK" },
            { 35856, "Receipt printer: paper end" },
            { 35857, "Receipt printer: paper low" },
            { 35858, "Receipt printer: general I/O error" },
            { 35859, "Receipt printer OK" },
            { 36355, "Timeout at BND command" },
            { 36356, "Error accessing a file" },
            { 36358, "No operational data" },
            { 36359, "Hardware state has changed" },
            { 36365, "No cash box found" },
            { 36366, "Cash box changed" },
            { 36368, "BND communication error" },
            { 36369, "BND communication restored" },
            { 36373, "No calibrating double detection" },
            { 36377, "Invalid user data (BNVCTRL.EXE)" },
            { 36380, "Banknote processing not ready" },
            { 36381, "BNV ready again" },
            { 36382, "BNV out of order (H4-Error)!" },
            { 36431, "Calibrating dual recognition" },
            { 36432, "GeldKarte transaction data cannot be transmitted" },
            { 36464, "IBP Event with no data" },
            { 36466, "IBP Record not found" },
            { 36467, "IBP Powerfail detected" },
            { 36468, "IBP Not ready" },
            { 36469, "IBP Terminal not found" },
            { 36470, "IBP No S&B card" },
            { 36471, "IBP Unknown card type" },
            { 36472, "IBP No barcode in database" },
            { 36473, "Power failure detected" },
            { 36474, "IBP Barcode is corrupt" },
            { 36480, "IBP Cannot insert barcode into database" },
            { 36481, "IBP Cannot update barcode to database" },
            { 36482, "IBP Luhn not correct" },
            { 36483, "IBP Cannot insert profile into database" },
            { 36484, "IBP Tariff time not correct" },
            { 36485, "IBP Cannot delete barcode from database" },
            { 36486, "IBP Cannot find barcode in PTCPTT" },
            { 36487, "IBP Wrong central computer number in barcode" },
            { 36488, "IBP Wrong owner number in barcode" },
            { 36489, "IBP Barcode already exist" },
            { 36496, "IBP No connection to central computer" },
            { 36497, "IBP No profile in database" },
            { 36498, "IBP Cannot add barcode to database" },
            { 36499, "IBP Cannot add barcode to database" },
            { 36506, "IBP Error during BCIMP" },
            { 36507, "IBP No default data in database" },
            { 36508, "IBP Wrong database command" },
            { 36509, "IBP Wrong card type" },
            { 36510, "IBP Wrong backup instance" },
            { 36511, "IBP Wrong normal instance" },
            { 36704, "Euro test failed!" },
            { 36705, "Euro test successful!" },
            { 36736, "Database error found" },
            { 36737, "Database file has reached a warning level" },
            { 36738, "Database table has reached a warning level" },
            { 36739, "Disk filled over limit!" },
            { 36740, "Bad sectors detected on hard disk drive" },
            { 36741, "Database transaction backup has reached a warning level" },
            { 36742, "Database transaction backup is switched off" },
            { 36743, "Hard disk drive has detected a mirroring error" },
            { 36744, "Database backup has detected an error" },
            { 36745, "Database backup to a second medium faulty" },
            { 36746, "Exceptional database error detected" },
            { 36747, "Working memory (RAM) has reached a warning level" },
            { 36748, "Validity of a licence has reached a warning level" },
            { 36749, "ZRmon: found temporary licenses!" },
            { 36751, "ZRMon: Database Encryption Keys expired, call Administrator!" },
            { 36768, "Error transferring electronic journal data" },
            { 36784, "Di Server: Start error! Check Di.ini" },
            { 36785, "Data import has no database connection" },
            { 36786, "Data import cannot open a file" },
            { 36791, "Data import error" },
            { 36793, "Data import cannot generate response file" },
            { 36794, "Data import cannot prepare import" },
            { 36795, "Data import cannot complete import" },
            { 36796, "Data import cannot complete import" },
            { 36797, "Data import cannot complete import" },
            { 36798, "Data import cannot verify check sum" },
            { 36799, "Data import cannot verify check sum" },
            { 36800, "Data import cannot verify check sum" },
            { 36801, "DI:Full Update aborted: Not enough records" },
            { 40960, "Receipt acknowledged of money processing error messages" },
            { 40976, "Data manually changed" },
            { 40977, "Service login" },
            { 40978, "Service log out" },
            { 40979, "Wrong password entered at the device" },
            { 40980, "Text_G: 51463/515/0" },
            { 40981, "Euro conversion started manually" },
            { 40982, "Manual euro conversion finished" },
            { 40984, "GeldKarte: Debiting error" },
            { 41065, "GeldKarte: Chip error" },
            { 41120, "GeldKarte: Debiting error, debit not yet executed" },
            { 41200, "GeldKarte: Debiting error" },
            { 41216, "Device is set out of operation" },
            { 41217, "Device is set into operation" },
            { 41218, "Device door is open" },
            { 41219, "Device door is closed" },
            { 41220, "Burglary alarm, device door open" },
            { 41221, "Burglary alarm is reset, device door is closed" },
            { 41222, "Device stand-alone" },
            { 41223, "Device networked" },
            { 41224, "No user authorization" },
            { 41225, "First life check message from field device" },
            { 41226, "Network ZR<->LR Offline" },
            { 41227, "Network ZR<->LR Online" },
            { 41228, "Event 0xA10C/41228" },
            { 41229, "Event 0xA10B/41229" },
            { 41232, "Ticket collection box full" },
            { 41233, "Ticket collection box emptied" },
            { 41234, "Reset ticket no. 1" },
            { 41235, "Reset ticket no. 2" },
            { 41236, "Reset ticket no. 3" },
            { 41237, "Reset ticket no. 4" },
            { 41242, "Ticket request with full check" },
            { 41243, "Ticket request without full check" },
            { 41244, "Entry full for hourly parkers" },
            { 41245, "Entry free for hourly parkers" },
            { 41246, "Entrance full for single ticket" },
            { 41247, "Entrance free for single ticket" },
            { 41248, "Status control 1 (201) on" },
            { 41249, "Status control 1 (201) off" },
            { 41250, "Status control 2 (202) on" },
            { 41251, "Status control 2 (202) off" },
            { 41252, "Status control 3 (203) on" },
            { 41253, "Status control 3 (203) off" },
            { 41254, "Status control 4 (204) on" },
            { 41255, "Status control 4 (204) off" },
            { 41256, "Status control 5 (205) on" },
            { 41257, "Status control 6 (206) on" },
            { 41258, "Status control 7 (207) on" },
            { 41259, "Status control 8 (208) on" },
            { 41264, "Data updating" },
            { 41265, "Camera on" },
            { 41266, "Camera off" },
            { 41267, "Intercom on" },
            { 41268, "Intercom off" },
            { 41269, "100% check on" },
            { 41270, "100% check off" },
            { 41271, "100% check data" },
            { 41272, "Card control data" },
            { 41273, "Miscounting" },
            { 41274, "Network CC-AG offline" },
            { 41280, "Reset via reset key" },
            { 41281, "Reset via mains connected (mains disconn. is registered)" },
            { 41282, "Reset via command" },
            { 41283, "Reset via restart 7 (RST 7 Command FFH)" },
            { 41284, "Reset via master init." },
            { 41289, "Reset via zeroing" },
            { 41290, "Reset money processing" },
            { 41291, "Reset credit card processing" },
            { 41296, "1 Device out of operation" },
            { 41297, "2 Device out of operation" },
            { 41298, "3 Device out of operation" },
            { 41299, "4 Device out of operation" },
            { 41300, "5 Device out of operation" },
            { 41301, "6 Device out of operation" },
            { 41302, "7 Device out of operation" },
            { 41303, "8 Device out of operation" },
            { 41304, "9 Device out of operation" },
            { 41305, "10 Device out of operation" },
            { 41306, "11 Device out of operation" },
            { 41307, "12 Device out of operation" },
            { 41308, "13 Device out of operation" },
            { 41309, "14 Device out of operation" },
            { 41310, "15 Device out of operation" },
            { 41311, "16 Device out of operation" },
            { 41312, "Device out of oper. in init." },
            { 41313, "Missing computer no. in initialization phase" },
            { 41314, "Missing device no. in initialization phase" },
            { 41315, "Missing operator no. in initialization phase" },
            { 41328, "Power off at device. Device is out of operation" },
            { 41330, "Wake up device" },
            { 41331, "PM - Sleep mode" },
            { 41332, "PM - Active mode" },
            { 41344, "Initialization phase terminated" },
            { 41360, "Power on" },
            { 41376, "Paper end in all stacks SL20" },
            { 41392, "BND deactivated" },
            { 41393, "BND activated" },
            { 41394, "Cash payment enabled" },
            { 41395, "Cash payment disabled" },
            { 41408, "PGL door open" },
            { 41409, "PGL door closed" },
            { 41410, "PGL hood open" },
            { 41411, "PGL hood closed" },
            { 41412, "Barrier door open" },
            { 41413, "Barrier door closed" },
            { 41414, "Barrier: UPS battery low" },
            { 41415, "Barrier: UPS battery normal" },
            { 41416, "Device overheated (air conditioning damaged)" },
            { 41417, "Device temperature normal" },
            { 41418, "Device on UPS power" },
            { 41419, "Device on mains power" },
            { 41440, "Operat.data error 1 (magnetic tickets article table not realistic)" },
            { 41441, "Operat.data error 2 (run off control not realistic)" },
            { 41442, "Operat.data error 3 (table of interchanging magnetic tickets not realistic)" },
            { 41443, "Operat.data error 4" },
            { 41444, "Operat.data error 5" },
            { 41445, "Operat.data error 6" },
            { 41446, "Operat.data error 7" },
            { 41447, "Operat.data error 8" },
            { 41448, "Operat.data error 9" },
            { 41449, "Operat.data error 10" },
            { 41450, "Operat.data error 11" },
            { 41451, "Operat.data error 12" },
            { 41452, "Operat.data error 13" },
            { 41453, "Operat.data error 14" },
            { 41454, "Operat.data error 15" },
            { 41455, "Operat.data error 16" },
            { 41456, "Operat.data error 17" },
            { 41457, "Operat.data error 18" },
            { 41458, "Operat.data error 19" },
            { 41459, "Operat.data error 20" },
            { 41460, "Operat.data error 21" },
            { 41461, "Operat.data error 22" },
            { 41462, "Operat.data error 23" },
            { 41463, "Operat.data error 24" },
            { 41464, "Operat.data error 25" },
            { 41465, "Operat.data error 26" },
            { 41466, "Operat.data error 27" },
            { 41467, "Operat.data error 28" },
            { 41468, "Operat.data error 29" },
            { 41469, "Operat.data error 30" },
            { 41470, "Operat.data error 31" },
            { 41471, "Operat.data error 32" },
            { 41472, "Initialization of the money processing completed" },
            { 41473, "Initialization of the magnetic ticket processing completed" },
            { 41474, "Initialization of the credit card processing completed" },
            { 41488, "Shift settlement" },
            { 41489, "Shift settlement start" },
            { 41490, "Shift settlement end" },
            { 41491, "Error emptying escrow" },
            { 41492, "Shift settlement has not been printed yet" },
            { 41493, "Shift settlement error" },
            { 41494, "Error upon extra booking of shift settlement" },
            { 41495, "No device shift open for booking" },
            { 41496, "At least 1 device shift open for booking" },
            { 41536, "Account locked after several login tries" },
            { 41728, "Journal printer: paper low" },
            { 41729, "Receipt printer: paper low" },
            { 41730, "Journal printer: paper end" },
            { 41731, "Receipt printer: paper end" },
            { 41732, "Journal printer ready" },
            { 41733, "Receipt printer ready" },
            { 41734, "Printer offline" },
            { 41735, "Printer online" },
            { 41736, "Journal printer: general IO error" },
            { 41737, "Receipt printer: general IO error" },
            { 41744, "Fiscal printer ready" },
            { 41745, "FP_Event 0xA311" },
            { 41746, "Time difference between POS and fiscal printer" },
            { 41747, "Time difference between POS and fiscal printer too large" },
            { 41748, "Fiscal printer detected unsupported communication protocol" },
            { 41750, "Device reports fiscal printer being out of paper" },
            { 41751, "Device reports no communication to fiscal printer" },
            { 41752, "Fiscal printer reports a receipt production error" },
            { 41753, "Fiscal printer: Shift should be closed" },
            { 41760, "Fiscal printer is waiting for time synchronisation" },
            { 41984, "Stack 1: Magnetic cards low" },
            { 41985, "Stack 1: Magnetic cards end" },
            { 41986, "Stack 1: Magnetic cards refilled" },
            { 41987, "Stack 2: Magnetic cards low (in preparation)" },
            { 41988, "Stack 2: Magnetic cards end (in preparation)" },
            { 41989, "Stack 2: Magnetic cards refilled (in preparation)" },
            { 41990, "Stack 3: Magnetic cards low (in preparation)" },
            { 41991, "Stack 3: Magnetic cards end (in preparation)" },
            { 41992, "Stack 3: Magnetic cards refilled (in preparation)" },
            { 41993, "Stack 4: Magnetic cards low (in preparation)" },
            { 41994, "Stack 4: Magnetic cards end (in preparation)" },
            { 41995, "Stack 4: Magnetic cards refilled (in preparation)" },
            { 41996, "Stack 5: Magnetic cards low (in preparation)" },
            { 41997, "Stack 5: Magnetic cards end (in preparation)" },
            { 41998, "Stack 5: Magnetic cards refilled (in preparation)" },
            { 41999, "Stack 6: Magnetic cards low (in preparation)" },
            { 42000, "Stack 6: Magnetic cards end (in preparation)" },
            { 42001, "Stack 6: Magnetic cards refilled (in preparation)" },
            { 42002, "Stack 7: Magnetic cards low (in preparation)" },
            { 42003, "Stack 7: Magnetic cards end (in preparation)" },
            { 42004, "Stack 7: Magnetic cards refilled (in preparation)" },
            { 42005, "Stack 8: Magnetic cards low (in preparation)" },
            { 42006, "Stack 8: Magnetic cards end (in preparation)" },
            { 42007, "Stack 8: Magnetic cards refilled (in preparation)" },
            { 42016, "Initial position / free-running of the TS-BG-0" },
            { 42017, "Produce service ticket at the TS-BG-0" },
            { 42018, "Ticket length correction at the TS-BG-0" },
            { 42019, "Grind sensitive comb at the TS-BG-0" },
            { 42020, "Adjust sensitive comb (convergence mark) of the TS-BG-0" },
            { 42021, "Clean / polish magnetic head at TS-BG-0" },
            { 42022, "Self-cleaning of sensitive comb at TS-BG-0" },
            { 42026, "General malfunction at the TS-BG-0 or TS-BG-0 - inoperative" },
            { 42027, "Magnetic data reading error at the TS-BG-0" },
            { 42032, "Initial position / free-running at the TS-BG-1" },
            { 42033, "Produce service ticket at the TS-BG-1" },
            { 42034, "Ticket length correction at the TS-BG-1" },
            { 42035, "Grind sensitive comb at the TS-BG-1" },
            { 42036, "Adjust sensitive comb (convergence mark) at the TS-BG-1" },
            { 42037, "Clean / polish magnetic head at the TS-BG-1" },
            { 42038, "Self-cleaning of sensitive comb at TS-BG-1" },
            { 42042, "General malfunction of the TS-BG-1 or TS-BG-1 - inoperative" },
            { 42043, "Magnetic data reading error of the TS-BG-1" },
            { 42048, "Reset at reader/writer" },
            { 42240, "Barrier 1 locked in open position" },
            { 42241, "Barrier 1 unlocked" },
            { 42242, "Barrier arm out of mounting bracket" },
            { 42243, "Barrier arm is back in mounting bracket" },
            { 42244, "Barrier arm in wrong position" },
            { 42245, "Barrier arm in correct position" },
            { 42246, "Barrier open for too long time" },
            { 42247, "Barrier open for too long time repaired" },
            { 42250, "Barrier 1 opened manually" },
            { 42251, "Barrier 1 closed manually" },
            { 42252, "Status control 3 (203) on" },
            { 42253, "Barrier opened after hitting an obstacle" },
            { 42254, "Barrier 1 locked in open position at device" },
            { 42255, "Barrier 1 unlocked at device" },
            { 42256, "Barrier 2 locked in open position" },
            { 42257, "Barrier 2 unlocked" },
            { 42258, "Barrier arm 2 off" },
            { 42259, "A44B-50570" },
            { 42260, "Barrier arm 2 in wrong position" },
            { 42261, "Barrier arm 2 in correct position" },
            { 42262, "Barrier 2 open for too long time" },
            { 42263, "Barrier 2 open for too long time repaired" },
            { 42266, "Barrier 2 opened manually" },
            { 42267, "Barrier 2 closed manually" },
            { 42272, "Turnstile 1 open locked" },
            { 42273, "Turnstile 1 unlocked" },
            { 42282, "Turnstile 1 opened manually" },
            { 42283, "Turnstile 1 closed manually" },
            { 42288, "Turnstile 2 open locked" },
            { 42289, "Turnstile 2 released" },
            { 42298, "Turnstile 2 opened manually" },
            { 42299, "Turnstile 2 closed manually" },
            { 42304, "Door 1 locked in open position" },
            { 42305, "Door 1 released" },
            { 42310, "Door 1 open for too long time" },
            { 42314, "Door 1 open" },
            { 42320, "Door 2 locked in open position" },
            { 42321, "Door 2 unlocked" },
            { 42326, "Door 2 open for too long time" },
            { 42327, "Door 2 open for too long time repaired" },
            { 42330, "Door 2 open" },
            { 42336, "Loops: E:o B:o C:o" },
            { 42337, "Loops: E:o B:o C:x" },
            { 42338, "Loops: E:o B:x C:o" },
            { 42339, "Loops: E:o B:x C:x" },
            { 42340, "Loops: E:x B:o C:o" },
            { 42341, "Loops: E:x B:o C:x" },
            { 42342, "Loops: E:x B:x C:o" },
            { 42343, "Loops: E:x B:x C:x" },
            { 42344, "Presence loop too long occupied" },
            { 42346, "Entry while barrier open" },
            { 42347, "Entry while barrier blocked" },
            { 42348, "Entry while barrier arm broken" },
            { 42349, "Exit while barrier open" },
            { 42350, "Exit while barrier blocked" },
            { 42351, "Exit while barrier arm broken" },
            { 42352, "Barrier open" },
            { 42353, "Barrier closed" },
            { 42354, "Undefined barrier arm position" },
            { 42496, "Std. component 1 requires maintenance" },
            { 42497, "Std. component 1 serviced" },
            { 42498, "Std. component 2 requires maintenance" },
            { 42499, "Std. component 2 serviced" },
            { 42500, "Std. component 3 requires maintenance" },
            { 42501, "Std. component 3 serviced" },
            { 42502, "Std. component 4 requires maintenance" },
            { 42503, "Std. component 4 serviced" },
            { 42504, "Std. component 5 requires maintenance" },
            { 42505, "Std. component 5 serviced" },
            { 42506, "Std. component 6 requires maintenance" },
            { 42507, "Std. component 6 serviced" },
            { 42508, "Std. component 7 requires maintenance" },
            { 42509, "Std. component 7 serviced" },
            { 42510, "Std. component 8 requires maintenance" },
            { 42511, "Std. component 8 serviced" },
            { 42512, "Std. component 9 requires maintenance" },
            { 42513, "Std. component 9 serviced" },
            { 42514, "Std. component 10 requires maintenance" },
            { 42515, "Std. component 10 serviced" },
            { 42516, "Std. component 11 requires maintenance" },
            { 42517, "Std. component 11 serviced" },
            { 42518, "Std. component 12 requires maintenance" },
            { 42519, "Std. component 12 serviced" },
            { 42520, "Std. component 13 requires maintenance" },
            { 42521, "Std. component 13 serviced" },
            { 42522, "Std. component 14 requires maintenance" },
            { 42523, "Std. component 14 serviced" },
            { 42524, "Std. component 15 requires maintenance" },
            { 42525, "Std. component 15 serviced" },
            { 42526, "Std. component 16 requires maintenance" },
            { 42527, "Std. component 16 serviced" },
            { 42751, "Maintenance counter request to field device" },
            { 42752, "Add-on component 1 requires maintenance" },
            { 42753, "Add-on component 1 serviced" },
            { 42754, "Add-on component 2 requires maintenance" },
            { 42755, "Add-on component 2 serviced" },
            { 42756, "Add-on component 3 requires maintenance" },
            { 42757, "Add-on component 3 serviced" },
            { 42758, "Add-on component 4 requires maintenance" },
            { 42759, "Add-on component 4 serviced" },
            { 42760, "Add-on component 5 requires maintenance" },
            { 42761, "Add-on component 5 serviced" },
            { 42762, "Add-on component 6 requires maintenance" },
            { 42763, "Add-on component 6 serviced" },
            { 42764, "Add-on component 7 requires maintenance" },
            { 42765, "Add-on component 7 serviced" },
            { 42766, "Add-on component 8 requires maintenance" },
            { 42767, "Add-on component 8 serviced" },
            { 42768, "Add-on component 9 requires maintenance" },
            { 42769, "Add-on component 9 serviced" },
            { 42770, "Add-on component 10 requires maintenance" },
            { 42771, "Add-on component 10 serviced" },
            { 42772, "Add-on component 11 requires maintenance" },
            { 42773, "Add-on component 11 serviced" },
            { 42774, "Add-on component 12 requires maintenance" },
            { 42775, "Add-on component 12 serviced" },
            { 42776, "Add-on component 13 requires maintenance" },
            { 42777, "Add-on component 13 serviced" },
            { 42778, "Add-on component 14 requires maintenance" },
            { 42779, "Add-on component 14 serviced" },
            { 42780, "Add-on component 15 requires maintenance" },
            { 42781, "Add-on component 15 serviced" },
            { 42782, "Add-on component 16 requires maintenance" },
            { 42783, "Add-on component 16 serviced" },
            { 42784, "Add-on component 1 malfunction" },
            { 42785, "Add-on component 1 malfunction repaired" },
            { 42786, "Add-on component 2 malfunction" },
            { 42787, "Add-on component 2 malfunction repaired" },
            { 42788, "Add-on component 3 malfunction" },
            { 42789, "Add-on component 3 malfunction repaired" },
            { 42790, "Add-on component 4 malfunction" },
            { 42791, "Add-on component 4 malfunction repaired" },
            { 42792, "Add-on component 5 malfunction" },
            { 42793, "Add-on component 5 malfunction repaired" },
            { 42794, "Add-on component 6 malfunction" },
            { 42795, "Add-on component 6 malfunction repaired" },
            { 42796, "Add-on component 7 malfunction" },
            { 42797, "Add-on component 7 malfunction repaired" },
            { 42798, "Add-on component 8 malfunction" },
            { 42799, "Add-on component 8 malfunction repaired" },
            { 42800, "Add-on component 9 malfunction" },
            { 42801, "Add-on component 9 malfunction repaired" },
            { 42802, "Add-on component 10 malfunction" },
            { 42803, "Add-on component 10 malfunction repaired" },
            { 42804, "Add-on component 11 malfunction" },
            { 42805, "Add-on component 11 malfunction repaired" },
            { 42806, "Add-on component 12 malfunction" },
            { 42807, "Add-on component 12 malfunction repaired" },
            { 42808, "Add-on component 13 malfunction" },
            { 42809, "Add-on component 13 malfunction repaired" },
            { 42810, "Add-on component 14 malfunction" },
            { 42811, "Add-on component 14 malfunction repaired" },
            { 42812, "Add-on component 15 malfunction" },
            { 42813, "Add-on component 15 malfunction repaired" },
            { 42814, "Add-on component 16 malfunction" },
            { 42815, "Add-on component 16 malfunction repaired" },
            { 43007, "Add-on component installation amendment" },
            { 43008, "Change low - pay exact" },
            { 43009, "Change low - pay exact remedied" },
            { 43010, "Coin box nearly full" },
            { 43011, "Coin box full" },
            { 43012, "Coin box removed" },
            { 43013, "Coin box removed" },
            { 43024, "CS 1: Low" },
            { 43025, "CS 2: Low" },
            { 43026, "CS 3: Low" },
            { 43027, "CS 4: Low" },
            { 43028, "CS 5: Low" },
            { 43029, "CS 6: Low" },
            { 43030, "CS 7 low" },
            { 43031, "CS 8 low" },
            { 43032, "CS 1: Low remedied" },
            { 43033, "CS 2: Low remedied" },
            { 43034, "CS 3: Low remedied" },
            { 43035, "CS 4: Low remedied" },
            { 43036, "CS 5: Low remedied" },
            { 43037, "CS 6: Low remedied" },
            { 43038, "CS 7 low remedied" },
            { 43039, "CS 8 low remedied" },
            { 43040, "CS 1 not available" },
            { 43041, "CS 2 not available" },
            { 43042, "CS 3 not available" },
            { 43043, "CS 4 not available" },
            { 43044, "CS 5 not available" },
            { 43045, "CS 6 not available" },
            { 43046, "CS 7 not available" },
            { 43047, "CS 8 not available" },
            { 43048, "CS 1 available" },
            { 43049, "CS 2 available" },
            { 43050, "CS 3 available" },
            { 43051, "CS 4 available" },
            { 43052, "CS 5 available" },
            { 43053, "CS 6 available" },
            { 43054, "CS 7 available" },
            { 43055, "CS 8 available" },
            { 43056, "CS 1: Signal of acceptance sensor is too long" },
            { 43057, "CS 2: Signal of acceptance sensor is too long" },
            { 43058, "CS 3: Signal of acceptance sensor is too long" },
            { 43059, "CS 4: Signal of acceptance sensor is too long" },
            { 43060, "CS 5: Signal of acceptance sensor is too long" },
            { 43061, "CS 6: Signal of acceptance sensor is too long" },
            { 43062, "CS 7: Signal of acceptance sensor is too long" },
            { 43063, "CS 8: Signal of acceptance sensor is too long" },
            { 43064, "CS 1: Signal of overflow sensor is too long" },
            { 43065, "CS 2: Signal of overflow sensor is too long" },
            { 43066, "CS 3: Signal of overflow sensor is too long" },
            { 43067, "CS 4: Signal of overflow sensor is too long" },
            { 43068, "CS 5: Signal of overflow sensor is too long" },
            { 43069, "CS 6: Signal of overflow sensor is too long" },
            { 43070, "CS 7: Signal of overflow sensor is too long" },
            { 43071, "CS 8: Signal of overflow sensor is too long" },
            { 43072, "CS 1: Signal of change store sensor too long - check in initial position" },
            { 43073, "CS 2: Signal of change store sensor too long - check in initial position" },
            { 43074, "CS 3: Signal of change store sensor too long - check in initial position" },
            { 43075, "CS 4: Signal of change store sensor too long - check in initial position" },
            { 43076, "CS 5: Signal of change store sensor too long - check in initial position" },
            { 43077, "CS 6: Signal of change store sensor too long - check in initial position" },
            { 43078, "CS 7: Signal of change store sensor too long - check in initial position" },
            { 43079, "CS 8: Signal of change store sensor too long - check in initial position" },
            { 43088, "CS 1: Blockage of lower worm cannot be dissolved" },
            { 43089, "CS 2: Blockage of lower worm cannot be dissolved" },
            { 43090, "CS 3: Blockage of lower worm cannot be dissolved" },
            { 43091, "CS 4: Blockage of lower worm cannot be dissolved" },
            { 43092, "CS 5: Blockage of lower worm cannot be dissolved" },
            { 43093, "CS 6: Blockage of lower worm cannot be dissolved" },
            { 43094, "CS 7: Blockage of lower worm cannot be dissolved" },
            { 43095, "CS 8: Blockage of lower worm cannot be dissolved" },
            { 43096, "CS 1: Blockage of upper worm cannot be dissolved" },
            { 43097, "CS 2: Blockage of upper worm cannot be dissolved" },
            { 43098, "CS 3: Blockage of upper worm cannot be dissolved" },
            { 43099, "CS 4: Blockage of upper worm cannot be dissolved" },
            { 43100, "CS 5: Blockage of upper worm cannot be dissolved" },
            { 43101, "CS 6: Blockage of upper worm cannot be dissolved" },
            { 43102, "CS 7: Blockage of upper worm cannot be dissolved" },
            { 43103, "CS 8: Blockage of upper worm cannot be dissolved" },
            { 43104, "CS 1: Initial position of lower worm is not reached" },
            { 43105, "CS 2: Initial position of lower worm is not reached" },
            { 43106, "CS 3: Initial position of lower worm is not reached" },
            { 43107, "CS 4: Initial position of lower worm is not reached" },
            { 43108, "CS 5: Initial position of lower worm is not reached" },
            { 43109, "CS 6: Initial position of lower worm is not reached" },
            { 43110, "CS 7: Initial position of lower worm is not reached" },
            { 43111, "CS 8: Initial position of lower worm is not reached" },
            { 43120, "CS 1: Uncontrolled movement" },
            { 43121, "CS 2: Uncontrolled movement" },
            { 43122, "CS 3: Uncontrolled movement" },
            { 43123, "CS 4: Uncontrolled movement" },
            { 43124, "CS 5: Uncontrolled movement" },
            { 43125, "CS 6: Uncontrolled movement" },
            { 43126, "CS 7: Uncontrolled movement" },
            { 43127, "CS 8: Uncontrolled movement" },
            { 43136, "CS 1: Change money distribution not complete" },
            { 43137, "CS 2: Change money distribution not complete" },
            { 43138, "CS 3: Change money distribution not complete" },
            { 43139, "CS 4: Change money distribution not complete" },
            { 43140, "CS 5: Change money distribution not complete" },
            { 43141, "CS 6: Change money distribution not complete" },
            { 43142, "CS 7: Change money distribution not complete" },
            { 43143, "CS 8: Change money distribution not complete" },
            { 43144, "Empty 1st change store" },
            { 43145, "Empty 2nd change store" },
            { 43150, "Empty 7th change store" },
            { 43151, "Empty 8th change store" },
            { 43168, "Coin signal of coin acceptor (channel 1) too long" },
            { 43169, "Coin signal of coin acceptor (channel 2) too long" },
            { 43170, "Coin signal of coin acceptor (channel 3) too long" },
            { 43171, "Coin signal of coin acceptor (channel 4) too long" },
            { 43172, "Coin signal of coin acceptor (channel 5) too long" },
            { 43173, "Coin signal of coin acceptor (channel 6) too long" },
            { 43174, "Coin signal of coin acceptor (channel 7) too long" },
            { 43175, "Coin signal of coin acceptor (channel 8) too long" },
            { 43176, "Coin signal of coin acceptor (channel 9) too long" },
            { 43177, "Coin signal of coin acceptor (channel 10) too long" },
            { 43178, "Coin signal of coin acceptor (channel 11) too long" },
            { 43179, "Coin signal of coin acceptor (channel 12) too long" },
            { 43180, "Coin signal of coin acceptor (channel 13) too long" },
            { 43181, "Coin signal of coin acceptor (channel 14) too long" },
            { 43182, "Coin signal of coin acceptor (channel 15) too long" },
            { 43183, "Coin signal of coin acceptor (channel 16) too long" },
            { 43200, "Coin path error in the intake of the pre-channel" },
            { 43201, "Coin path error in the outlet of the pre-channel" },
            { 43202, "Error upon the coin size check in the pre-channel" },
            { 43216, "Initial worm position in the escrow upon coin acceptance" },
            { 43217, "Initial position of the cashier lever in the escrow upon coin acceptance" },
            { 43218, "Wrong sensor information at the intake of the escrow" },
            { 43219, "Initial worm position in the escrow is not reached upon cashiering" },
            { 43220, "Initial cashier lever position of the escrow is not reached" },
            { 43232, "Text 51353 is missing" },
            { 43248, "Empty all change stores" },
            { 43249, "Empty escrow" },
            { 43253, "Emptying of change store completed" },
            { 43254, "Emptying of escrow completed" },
            { 43264, "Banknote box nearly full" },
            { 43265, "Banknote box full" },
            { 43266, "Banknote box removed" },
            { 43267, "Banknote box inserted" },
            { 43280, "Banknote dispenser removed" },
            { 43281, "Banknote dispenser inserted" },
            { 43282, "BND: Banknotes low" },
            { 43283, "BND: Banknotes low remedied" },
            { 43284, "BND: No banknotes available" },
            { 43285, "BND: Banknotes available again" },
            { 43296, "Banknote escrow reached maximum value" },
            { 43536, "Balance main store is not updated or erroneous!" },
            { 43537, "CRC error in operational data of the money processing" },
            { 43552, "Faulty erase of the shift data" },
            { 43776, "Do not tolerate card check" },
            { 43777, "Tolerate card check" },
            { 43778, "Enable card negative evaluation" },
            { 43779, "Disable card negative evaluation" },
            { 43793, "Facility code not permitted" },
            { 43800, "Tariff booking was effected via magnetic ticket" },
            { 43803, "Number of season parker unknown" },
            { 43812, "Backout detected" },
            { 43824, "Interchanging article not available" },
            { 43825, "Residual point value not sufficient" },
            { 43826, "Congress card blocked by anti-passback time" },
            { 43840, "Card not permitted according to car park profile" },
            { 43842, "Card not permitted according to customer blocking code" },
            { 43843, "Article-get error" },
            { 43904, "Reserved for interruption code 150" },
            { 43905, "Reserved for interruption code 151" },
            { 43906, "Reserved for interruption code 152" },
            { 43907, "Reserved for interruption code 153" },
            { 43908, "Reserved for interruption code 154" },
            { 43909, "Reserved for interruption code 155" },
            { 43910, "Reserved for interruption code 156" },
            { 43911, "Reserved for interruption code 157" },
            { 43912, "Reserved for interruption code 158" },
            { 43913, "Reserved for interruption code 159" },
            { 43914, "Reserved for interruption code 160" },
            { 43915, "Reserved for interruption code 161" },
            { 43916, "Reserved for interruption code 162" },
            { 43917, "Reserved for interruption code 163" },
            { 43918, "Reserved for interruption code 164" },
            { 43919, "Reserved for interruption code 165" },
            { 43920, "Reserved for interruption code 166" },
            { 43921, "Reserved for interruption code 167" },
            { 43922, "Reserved for interruption code 168" },
            { 43923, "Reserved for interruption code 169" },
            { 43924, "Reserved for interruption code 170" },
            { 43925, "Reserved for interruption code 171" },
            { 43926, "Reserved for interruption code 172" },
            { 43927, "Reserved for interruption code 173" },
            { 43928, "Reserved for interruption code 174" },
            { 43929, "Reserved for interruption code 175" },
            { 43930, "Reserved for interruption code 176" },
            { 43931, "Reserved for interruption code 177" },
            { 43932, "Reserved for interruption code 178" },
            { 43933, "Reserved for interruption code 179" },
            { 43934, "Reserved for interruption code 180" },
            { 43935, "Reserved for interruption code 181" },
            { 43936, "Reserved for interruption code 182" },
            { 43937, "Reserved for interruption code 183" },
            { 43938, "Reserved for interruption code 184" },
            { 43939, "Reserved for interruption code 185" },
            { 43940, "Reserved for interruption code 186" },
            { 43941, "Reserved for interruption code 187" },
            { 43942, "Reserved for interruption code 188" },
            { 43943, "Reserved for interruption code 189" },
            { 43944, "Reserved for interruption code 190" },
            { 43945, "Reserved for interruption code 191" },
            { 43946, "Reserved for interruption code 192" },
            { 43947, "Reserved for interruption code 193" },
            { 43948, "Reserved for interruption code 194" },
            { 43949, "Reserved for interruption code 195" },
            { 43950, "Reserved for interruption code 196" },
            { 43951, "Reserved for interruption code 197" },
            { 43952, "Reserved for interruption code 198" },
            { 43953, "Reserved for interruption code 199" },
            { 43958, "Limit exceeded" },
            { 43959, "Interruption" },
            { 43960, "Wrong PIN too many times" },
            { 43961, "Card already encoded" },
            { 43962, "Card not encodable" },
            { 43963, "Data line disturbed" },
            { 43964, "Duplicate card invalid" },
            { 43965, "Customer not found" },
            { 43966, "Card has already been issued" },
            { 43967, "Customer blocking 24 Hrs." },
            { 43968, "Card reader busy" },
            { 43969, "Card not encoded" },
            { 43970, "Card not in the SBC" },
            { 43971, "Card reader not OK" },
            { 43972, "Article not permitted" },
            { 43973, "DES module faulty" },
            { 43974, "Data error" },
            { 43975, "Manual input at EP-10" },
            { 43976, "Card distribution not permitted" },
            { 43977, "Card still invalid" },
            { 43978, "Card OK and expired" },
            { 43979, "Data line disturbed" },
            { 43980, "Data line disturbed" },
            { 43981, "Interruption sales device" },
            { 43982, "Reserved for interruption code 228" },
            { 43983, "Wrong MAC check" },
            { 43984, "No Mac" },
            { 43985, "Credit impossible" },
            { 43986, "DES-System error1" },
            { 43987, "DES-System error2" },
            { 43988, "Return of goods not possible" },
            { 43989, "Wrong limit/product" },
            { 43990, "EC not possible" },
            { 43991, "Reserved for interruption code 237" },
            { 43992, "Reserved for interruption code 238" },
            { 43993, "Wrong system settings" },
            { 43994, "Reserved for interruption code 239" },
            { 43995, "Reserved for interruption code 240" },
            { 43996, "Reserved for interruption code 241" },
            { 43997, "Reserved for interruption code 242" },
            { 43998, "Reserved for interruption code 243" },
            { 43999, "Reserved for interruption code 244" },
            { 44000, "Reserved for interruption code 245" },
            { 44001, "Reserved for interruption code 246" },
            { 44002, "Reserved for interruption code 247" },
            { 44003, "Reserved for interruption code 248" },
            { 44004, "Reserved for interruption code 249" },
            { 44005, "Reserved for interruption code 250" },
            { 44006, "Reserved for interruption code 251" },
            { 44007, "Reserved for interruption code 252" },
            { 44008, "Reserved for interruption code 253" },
            { 44009, "Reserved for interruption code 254" },
            { 44010, "Reserved for interruption code 255" },
            { 44032, "General V11 dialogue error (no further classification)" },
            { 44033, "Dialogue event" },
            { 44034, "V11-#- error BNA-#- channel" },
            { 44035, "V11-#- error KD12-#- channel" },
            { 44049, "Erroneous buffer status when receiving (IC10-PH)" },
            { 44050, "Erroneous buffer status when transmitting (IC10-PH)" },
            { 44051, "IC10-PH Init." },
            { 44064, "SW Checksum PKA" },
            { 44065, "SW Checksum PMK" },
            { 44066, "SW Checksum PGL" },
            { 44067, "Lost ticket at PKA activated" },
            { 44068, "New shift at the automatic pay station" },
            { 44069, "New shift at the exit" },
            { 44072, "Function barcode exchange switched on" },
            { 44073, "Function barcode exchange switched off" },
            { 44074, "Barcode exchange becomes active because offline" },
            { 44075, "Barcode exchange inactive because of online" },
            { 44080, "Lost ticket REAL at PKA activated" },
            { 44085, "Magstripe checked: Not OK!" },
            { 44086, "Magstripe checked: OK!" },
            { 44087, "Checking Magstripe..." },
            { 44096, "MotionControl ON" },
            { 44097, "MotionControl OFF" },
            { 44098, "MotionControl Alarm" },
            { 44099, "MotionControl Reset" },
            { 44545, "Jam at exit activated" },
            { 44546, "Jam at exit deactivated" },
            { 44551, "Retain ChipCoin after payment switched on" },
            { 44552, "Retain ChipCoin after payment switched off" },
            { 44912, "GeldKarte: Internal module error" },
            { 45008, "GeldKarte: Internal module error" },
            { 45055, "No power supply" },
            { 45056, "Status request" },
            { 45072, "Set device out of operation" },
            { 45073, "Set device in operation" },
            { 45136, "BNV: initialization error" },
            { 45187, "Setting BNV parameters" },
            { 45188, "Reading BNV parameters" },
            { 45231, "Resetting money summation data" },
            { 45232, "Read money data" },
            { 45312, "Power on" },
            { 45313, "Power off" },
            { 45316, "Loud danger signal forced entry attempt" },
            { 45317, "Loud danger signal manipulation attempt" },
            { 45318, "Loud danger signal stopped after manipulation attempt" },
            { 45319, "User authorization OK" },
            { 45320, "No user authorization (wrong PIN)" },
            { 45321, "ex/in" },
            { 45322, "Reset without specification (no buffering ?)" },
            { 45323, "Voltage return after short voltage loss" },
            { 45324, "Reset money processing" },
            { 45325, "Connection breakdown upon data transfer" },
            { 45328, "Out of service status eliminated via online" },
            { 45329, "Out of service status eliminated via service man" },
            { 45330, "New operational data received online" },
            { 45338, "ZR was restarted" },
            { 45344, "CS 1 removed" },
            { 45345, "CS 2 removed" },
            { 45346, "CS 3 removed" },
            { 45347, "CS 4 removed" },
            { 45348, "CS 5 removed" },
            { 45349, "CS 6 removed" },
            { 45350, "CS 7 removed" },
            { 45351, "ACS 3 removed" },
            { 45352, "ACS 1 removed" },
            { 45353, "ACS 2 removed" },
            { 45354, "Not used" },
            { 45360, "CS 1 inserted" },
            { 45361, "CS 2 inserted" },
            { 45362, "CS 3 inserted" },
            { 45363, "CS 4 inserted" },
            { 45364, "CS 5 inserted" },
            { 45365, "CS 6 inserted" },
            { 45366, "CS 7 inserted" },
            { 45367, "ACS 3 inserted" },
            { 45368, "ACS 1 inserted" },
            { 45369, "ACS 2 inserted" },
            { 45370, "Not used" },
            { 45376, "Coin box removed" },
            { 45377, "Coin box inserted" },
            { 45378, "Banknote box removed" },
            { 45379, "Banknote box inserted" },
            { 45384, "Filling by coin slot" },
            { 45392, "Coin species 1 low" },
            { 45393, "Coin species 2 low" },
            { 45394, "Coin species 3 low" },
            { 45395, "Coin species 4 low" },
            { 45396, "Coin species 5 low" },
            { 45397, "Coin species 6 low" },
            { 45398, "Coin species 7 low" },
            { 45399, "Coin species 8 low" },
            { 45400, "Coin species 9 low" },
            { 45408, "Coin species 1 low remedied" },
            { 45409, "Coin species 2 low remedied" },
            { 45410, "Coin species 3 low remedied" },
            { 45411, "Coin species 4 low remedied" },
            { 45412, "Coin species 5 low remedied" },
            { 45413, "Coin species 6 low remedied" },
            { 45414, "Coin species 7 low remedied" },
            { 45415, "Coin species 8 low remedied" },
            { 45416, "Coin species 9 low remedied" },
            { 45443, "<03> BNV data set" },
            { 45444, "<04> Service command executed" },
            { 45445, "<05> Service command executed" },
            { 45446, "<06> Service command executed" },
            { 45447, "<07> Service command executed" },
            { 45448, "<08> Service command executed" },
            { 45449, "<09> Service command executed" },
            { 45450, "<10> Service command executed" },
            { 45451, "<11> Service command executed" },
            { 45452, "<12> Service command executed" },
            { 45453, "<13> Service command executed" },
            { 45454, "<14> Service command executed" },
            { 45455, "<15> Service command executed" },
            { 45456, "<16> Service command executed" },
            { 45457, "<17> Service command executed" },
            { 45458, "<18> Service command executed" },
            { 45459, "<19> Service command executed" },
            { 45460, "<20> Service command executed" },
            { 45461, "<21> Service command executed" },
            { 45462, "<22> Service command executed" },
            { 45463, "<23> Service command executed" },
            { 45464, "<24> Service command executed" },
            { 45465, "<25> Service command executed" },
            { 45466, "<26> Service command executed" },
            { 45467, "<27> Service command executed" },
            { 45468, "<28> Service command executed" },
            { 45469, "<29> Service command executed" },
            { 45470, "<30> Service command executed" },
            { 45471, "<31> Service command executed" },
            { 45472, "<32> Service command executed" },
            { 45473, "<33> Service command executed" },
            { 45474, "<34> Service command executed" },
            { 45475, "<35> Service command executed" },
            { 45476, "<36> Service command executed" },
            { 45477, "<37> Service command executed" },
            { 45478, "<38> Service command executed" },
            { 45479, "<39> Service command executed" },
            { 45480, "<40> Service command executed" },
            { 45481, "<41> Service command executed" },
            { 45482, "<42> Service command executed" },
            { 45483, "<43> Service command executed" },
            { 45484, "<44> Service command executed" },
            { 45485, "<45> Service command executed" },
            { 45486, "<46> Service command executed" },
            { 45487, "<47> Service command executed" },
            { 45488, "<48> Service command executed" },
            { 45489, "<49> Service command executed" },
            { 45490, "Event 0xB1B2/45490" },
            { 45491, "Event 0xB1B3/45491" },
            { 45492, "Event 0xB1B4/45492" },
            { 45493, "Event 0xB1B5/45493" },
            { 45494, "Event 0xB1B6/45494" },
            { 45495, "Event 0xB1B7/45495" },
            { 45496, "Event 0xB1B8/45496" },
            { 45497, "<57> Input contents ACS x" },
            { 45498, "<58> Service command executed" },
            { 45499, "<59> Input change money via coin insertion" },
            { 45500, "<60> Service command executed" },
            { 45501, "<61> Service command executed" },
            { 45502, "<62> Service command executed" },
            { 45503, "<63> Service command executed" },
            { 45504, "<64> Service command executed" },
            { 45505, "<65> Service command executed" },
            { 45506, "<66> Service command executed" },
            { 45507, "<67> Service command executed" },
            { 45508, "<68> Service command executed" },
            { 45509, "<69> Service command executed" },
            { 45510, "<70> Service command executed" },
            { 45511, "<71> Service command executed" },
            { 45512, "<72> Service command executed" },
            { 45513, "<73> Service command executed" },
            { 45514, "<74> Service command executed" },
            { 45515, "<75> Service command executed" },
            { 45516, "<76> Service command executed" },
            { 45517, "<77> Service command executed" },
            { 45518, "<78> Service command executed" },
            { 45519, "<79> Service command executed" },
            { 45520, "<80> Service command executed" },
            { 45521, "<81> Service command executed" },
            { 45522, "<82> Service command executed" },
            { 45523, "<83> Service command executed" },
            { 45524, "<84> Service command executed" },
            { 45525, "<85> Service command executed" },
            { 45526, "<86> Service command executed" },
            { 45527, "<87> Service command executed" },
            { 45528, "<88> Service command executed" },
            { 45529, "<89> Service command executed" },
            { 45530, "<90> Service command executed" },
            { 45531, "<91> Service command executed" },
            { 45532, "<92> Service command executed" },
            { 45533, "<93> Service command executed" },
            { 45534, "<94> Service command executed" },
            { 45535, "<95> Service command executed" },
            { 45536, "<96> Service command executed" },
            { 45537, "<97> Service command executed" },
            { 45538, "<98> Service command executed" },
            { 45539, "<99> Service command executed" },
            { 45567, "<99> Service command executed" },
            { 45569, "Text 51352 is missing" },
            { 45570, "Control \"Retrieve last ten payments\"" },
            { 45575, "Event for last ten movements from PGL" },
            { 45576, "Event for last ten movements from pay station" },
            { 45600, "XERROR 20: Bad release length" },
            { 45603, "XERROR 23: No buffer on high level" },
            { 45604, "XERROR 24: No buffer on low level" },
            { 45612, "XERROR 2C: 0FF CMD executed" },
            { 45617, "XERROR 31: Stack overflow" },
            { 45824, "Cash data request" },
            { 45825, "MKV Copies low stack 2" },
            { 45826, "MKV Copies low stack 3" },
            { 45904, "MKV Timeout : Init." },
            { 45905, "MKV Timeout : ....." },
            { 45906, "MKV Timeout : ....." },
            { 45907, "MKV Timeout : ....." },
            { 45936, "MKV Copies low end stack 1" },
            { 45937, "MKV Copies low end stack 2" },
            { 45938, "MKV Copies low end stack 3" },
            { 45953, "MKV Grp. 1 Transport problem upon intake" },
            { 45954, "MKV Grp. 1 Transport problem upon distribution" },
            { 45955, "MKV Grp. 1 Writing error due to transport problem" },
            { 45956, "MKV Grp. 1 Cannot be written at requested recording density" },
            { 45957, "MKV Grp. 1 Data set for writing remains under required length" },
            { 45958, "MKV Grp. 1 Block count size error upon writing (> 255)" },
            { 45959, "MKV Grp. 1 Reading error: Transport stopped before reading end" },
            { 45960, "MKV Grp. 1 No avaluative reading signals" },
            { 45961, "MKV Grp. 1 Unpermitted bit density upon reading" },
            { 45962, "MKV Grp. 1 Read after write faulty" },
            { 45967, "MKV Grp. 1 General transport error" },
            { 45968, "MKV Grp. 2 Thermal head cannot be lifted" },
            { 45969, "MKV Grp. 2 Transport problem upon intake" },
            { 45970, "MKV Grp. 2 Transport problem upon distribution" },
            { 45971, "MKV Grp. 2 Error generating print data" },
            { 45972, "MKV Grp. 2 Unknown command sent to printer" },
            { 45973, "MKV Grp. 2 Too many blanks" },
            { 45974, "MKV Grp. 2 No OPEN command" },
            { 45975, "MKV Grp. 2 No initial data" },
            { 45984, "MKV Grp. 3 Erroneous cutter movement" },
            { 45985, "MKV Grp. 3 Transport problem upon intake" },
            { 45986, "MKV Grp. 3 Transport problem upon distribution" },
            { 45987, "MKV Grp. 3 No tickets in stack 1" },
            { 45988, "MKV Grp. 3 No tickets in stack 2" },
            { 45989, "MKV Grp. 3 No tickets in stack 3" },
            { 45993, "MKV Grp. 3 Error upon copy reset" },
            { 45994, "MKV Grp. 3 Ticket edge cannot be found with the initial" },
            { 45995, "MKV Grp. 3 Malfunction of LB in front of the cutter" },
            { 45996, "MKV Grp. 3 Ticket edge not recognized" },
            { 45997, "MKV Grp. 3 Ticket separation between two tickets not found" },
            { 45998, "MKV Grp. 3 Cut ticket has not left the feeding" },
            { 45999, "MKV Grp. 3 Error at LB when leaving paper switch" },
            { 46000, "MKV Grp. 4 Error paper switch" },
            { 46112, "Tester 16 out of service" },
            { 47104, "Not enough change" },
            { 47105, "Filling completed" },
            { 47106, "Coins low in ACS 1" },
            { 47107, "Coins low in ACS 1 remedied" },
            { 47108, "Coins low in ACS 2" },
            { 47109, "Event 0xB805/47109" },
            { 47110, "Event 0xB806/47110" },
            { 47111, "ACS 1 blocked" },
            { 47112, "ACS 2 blocked" },
            { 47113, "Coins low in ACS 2 remedied" },
            { 47114, "Not used" },
            { 47115, "Coins low in ACS 3" },
            { 47116, "Coins low in ACS 3 remedied" },
            { 47117, "ACS 3 blocked" },
            { 47120, "MVA: Timeout: initial position after reset" },
            { 47121, "MVA: Timeout: termination" },
            { 47122, "MVA: Timeout: return" },
            { 47123, "MVA: Timeout: blocking" },
            { 47124, "MVA: Timeout: status request" },
            { 47125, "MVA: Timeout: change money distribution" },
            { 47128, "Pre-channel not in initial position" },
            { 47129, "Coin acceptor not free" },
            { 47136, "Coin acceptor (channel 1) permanant acceptance signal" },
            { 47137, "Coin acceptor (channel 2) permanant acceptance signal" },
            { 47138, "Coin acceptor (channel 3) permanant acceptance signal" },
            { 47139, "Coin acceptor (channel 4) permanant acceptance signal" },
            { 47140, "Coin acceptor (channel 5) permanant acceptance signal" },
            { 47141, "Coin acceptor (channel 6) permanant acceptance signal" },
            { 47142, "Coin acceptor (channel 7) permanant acceptance signal" },
            { 47143, "Coin acceptor (channel 8) permanant acceptance signal" },
            { 47144, "Coin acceptor (channel 9) permanant acceptance signal" },
            { 47145, "Coin acceptor (channel 10) permanant acceptance signal" },
            { 47146, "Coin acceptor (channel 11) permanant acceptance signal" },
            { 47147, "Coin acceptor (channel 12) permanant acceptance signal" },
            { 47148, "Coin acceptor (channel 13) permanant acceptance signal" },
            { 47149, "Coin acceptor (channel 14) permanant acceptance signal" },
            { 47150, "Coin acceptor (channel 15) permanant acceptance signal" },
            { 47151, "Coin acceptor (channel 16) permanant acceptance signal" },
            { 47152, "CS 1: Permanent signal at coin distributor / upper LB" },
            { 47153, "CS 2: Permanent signal at coin distributor / upper LB" },
            { 47154, "CS 3: Permanent signal at coin distributor / upper LB" },
            { 47155, "CS 4: Permanent signal at coin distributor / upper LB" },
            { 47156, "CS 5: Permanent signal at coin distributor / upper LB" },
            { 47157, "CS 6: Permanent signal at coin distributor / upper LB" },
            { 47160, "CS 1: Bouncing signals at coin distributor / upper LB" },
            { 47161, "CS 2: Bouncing signals at coin distributor / upper LB" },
            { 47162, "CS 3: Bouncing signals at coin distributor / upper LB" },
            { 47163, "CS 4: Bouncing signals at coin distributor / upper LB" },
            { 47164, "CS 5: Bouncing signals at coin distributor / upper LB" },
            { 47165, "CS 6: Bouncing signals at coin distributor / upper LB" },
            { 47168, "CS 1: Permanent signal at coin distributor / lower LB" },
            { 47169, "CS 2: Permanent signal at coin distributor / lower LB" },
            { 47170, "CS 3: Permanent signal at coin distributor / lower LB" },
            { 47171, "CS 4: Permanent signal at coin distributor / lower LB" },
            { 47172, "CS 5: Permanent signal at coin distributor / lower LB" },
            { 47173, "CS 6: Permanent signal at coin distributor / lower LB" },
            { 47176, "CS 1: Wrong intake signal" },
            { 47177, "CS 2: Wrong intake signal" },
            { 47178, "CS 3: Wrong intake signal" },
            { 47179, "CS 4: Wrong intake signal" },
            { 47180, "CS 5: Wrong intake signal" },
            { 47181, "CS 6: Wrong intake signal" },
            { 47184, "CS 1: Intake signal with running change store" },
            { 47185, "CS 2: Intake signal with running change store" },
            { 47186, "CS 3: Intake signal with running change store" },
            { 47187, "CS 4: Intake signal with running change store" },
            { 47188, "CS 5: Intake signal with running change store" },
            { 47189, "CS 6: Intake signal with running change store" },
            { 47192, "CS 1: Bouncing signals at coin distributor / lower LB" },
            { 47193, "CS 2: Bouncing signals at coin distributor / lower LB" },
            { 47194, "CS 3: Bouncing signals at coin distributor / lower LB" },
            { 47195, "CS 4: Bouncing signals at coin distributor / lower LB" },
            { 47196, "CS 5: Bouncing signals at coin distributor / lower LB" },
            { 47197, "CS 6: Bouncing signals at coin distributor / lower LB" },
            { 47200, "CS 1: Error in giving change" },
            { 47201, "CS 2: Error in giving change" },
            { 47202, "CS 3: Error in giving change" },
            { 47203, "CS 4: Error in giving change" },
            { 47204, "CS 5: Error in giving change" },
            { 47205, "CS 6: Error in giving change" },
            { 47206, "ACS 1: Error in giving change" },
            { 47207, "ACS 2: Error in giving change" },
            { 47208, "ASC 3: Error in giving change" },
            { 47216, "CS 1: Reverse motion timeout - LB dark" },
            { 47217, "CS 2: Reverse motion timeout - LB dark" },
            { 47218, "CS 3: Reverse motion timeout - LB dark" },
            { 47219, "CS 4: Reverse motion timeout - LB dark" },
            { 47220, "CS 5: Reverse motion timeout - LB dark" },
            { 47221, "CS 6: Reverse motion timeout - LB dark" },
            { 47224, "CS 1: Forward motion timeout - LB dark" },
            { 47225, "CS 2: Forward motion timeout - LB dark" },
            { 47226, "CS 3: Forward motion timeout - LB dark" },
            { 47227, "CS 4: Forward motion timeout - LB dark" },
            { 47228, "CS 5: Forward motion timeout - LB dark" },
            { 47229, "CS 6: Forward motion timeout - LB dark" },
            { 47232, "CS 1: Reverse motion timeout - LB bright" },
            { 47233, "CS 2: Reverse motion timeout - LB bright" },
            { 47234, "CS 3: Reverse motion timeout - LB bright" },
            { 47235, "CS 4: Reverse motion timeout - LB bright" },
            { 47236, "CS 5: Reverse motion timeout - LB bright" },
            { 47237, "CS 6: Reverse motion timeout - LB bright" },
            { 47240, "CS 1: Forward motion timeout - LB bright" },
            { 47241, "CS 2: Forward motion timeout - LB bright" },
            { 47242, "CS 3: Forward motion timeout - LB bright" },
            { 47243, "CS 4: Forward motion timeout - LB bright" },
            { 47244, "CS 5: Forward motion timeout - LB bright" },
            { 47245, "CS 6: Forward motion timeout - LB bright" },
            { 47248, "CS 1: Motor not started" },
            { 47249, "CS 2: Motor not started" },
            { 47250, "CS 3: Motor not started" },
            { 47251, "CS 4: Motor not started" },
            { 47252, "CS 5: Motor not started" },
            { 47253, "CS 6: Motor not started" },
            { 47256, "Emergency switch off 24 Volt" },
            { 47264, "CS 1: Timeout LB coin path -> dispensing tray" },
            { 47265, "CS 2: Timeout LB coin path -> dispensing tray" },
            { 47266, "CS 3: Timeout LB coin path -> dispensing tray" },
            { 47267, "CS 4: Timeout LB coin path -> dispensing tray" },
            { 47268, "CS 5: Timeout LB coin path -> dispensing tray" },
            { 47269, "CS 6: Timeout LB coin path -> dispensing tray" },
            { 47272, "CS 1: Timeout LB coin path -> coin box" },
            { 47273, "CS 2: Timeout LB coin path -> coin box" },
            { 47274, "CS 3: Timeout LB coin path -> coin box" },
            { 47275, "CS 4: Timeout LB coin path -> coin box" },
            { 47276, "CS 5: Timeout LB coin path -> coin box" },
            { 47277, "CS 6: Timeout LB coin path -> coin box" },
            { 47280, "Missing or defect at init: Pre-channel" },
            { 47281, "Missing or defect at init: Coin acceptor" },
            { 47282, "Missing or defect at init: Change store carrier" },
            { 47283, "Missing or defect at init: Switch in coin box" },
            { 47284, "Missing or defect at init: Coin box incasement" },
            { 47285, "Missing or defect at init: CS 1" },
            { 47286, "Missing or defect at init: CS 2" },
            { 47287, "Missing or defect at init: CS 3" },
            { 47288, "Missing or defect at init: CS 4" },
            { 47289, "Missing or defect at init: CS 5" },
            { 47290, "Missing or defect at init: CS 6" },
            { 47291, "Missing or defect at init: ACS 1" },
            { 47292, "Missing or defect at init: ACS 2" },
            { 47293, "Missing or defect at init: ACS removal" },
            { 47294, "Missing or defect at init: Lever cash removal." },
            { 47295, "Missing or defect at init: ACS 3" },
            { 47296, "Missing or defect prior to operation: Pre-channel" },
            { 47297, "Missing or defect prior to operation: Coin acceptor" },
            { 47298, "Missing or defect prior to operation: Change store carrier" },
            { 47299, "Missing or defect prior to operation: Switch in coin box" },
            { 47300, "Missing or defect prior to operation: Coin box incasement" },
            { 47301, "Missing or defect prior to operation: CS 1" },
            { 47302, "Missing or defect prior to operation: CS 2" },
            { 47303, "Missing or defect prior to operation: CS 3" },
            { 47304, "Missing or defect prior to operation: CS 4" },
            { 47305, "Missing or defect prior to operation: CS 5" },
            { 47306, "Missing or defect prior to operation: CS 6" },
            { 47307, "Missing or defect prior to operation: ACS 1" },
            { 47308, "Missing or defect prior to operation: ACS 2" },
            { 47309, "Missing or defect prior to operation: ACS 3" },
            { 47312, "Missing or defect at service: Pre-channel" },
            { 47313, "Missing or defect at service: Coin acceptor" },
            { 47314, "Missing or defect at service: Change store carrier" },
            { 47315, "Missing or defect at service: Switch in coin box" },
            { 47316, "Missing or defect at service: Coin box incasement" },
            { 47317, "Missing or defect at service: CS 1" },
            { 47318, "Missing or defect at service: CS 2" },
            { 47319, "Missing or defect at service: CS 3" },
            { 47320, "Missing or defect at service: CS 4" },
            { 47321, "Missing or defect at service: CS 5" },
            { 47322, "Missing or defect at service: CS 6" },
            { 47323, "Missing or defect at service: ASC 1" },
            { 47324, "Missing or defect at service: ACS 2" },
            { 47325, "Missing or defect at service: ACS 3" },
            { 47328, "Same coin box no. inserted upon exchange" },
            { 47329, "Exchange coin box w/o voltage" },
            { 47330, "CS 1: Exchange w/o voltage" },
            { 47331, "CS 2: Exchange w/o voltage" },
            { 47332, "CS 3: Exchange w/o voltage" },
            { 47333, "CS 4: Exchange w/o voltage" },
            { 47334, "CS 5: Exchange w/o voltage" },
            { 47335, "CS 6: Exchange w/o voltage" },
            { 47336, "ACS 1: Exchange w/o voltage" },
            { 47337, "ACS 2: Exchange w/o voltage" },
            { 47338, "ACS 3: Exchange w/o voltage" },
            { 47352, "Connection to ZCGVA is up again" },
            { 47353, "ZCGVA in initialization phase" },
            { 47356, "MVA in service" },
            { 47357, "MVA out of service" },
            { 47360, "Banknote box 90% full" },
            { 47361, "Banknote box 100% full" },
            { 47362, "Same banknote box inserted upon exchange" },
            { 47363, "Banknote box exchanged w/o voltage" },
            { 47364, "BNV error end" },
            { 47376, "BNV: Timeout error: Init. status request" },
            { 47377, "BNV: Timeout error: cashiering" },
            { 47378, "BNV: Timeout error: blocking" },
            { 47379, "BNV: Timeout error: return" },
            { 47380, "Connection V11 BV12 upon init." },
            { 47381, "Connection V11 BV12" },
            { 47616, "BNV: Wrong command" },
            { 47617, "BNV: Invalid command" },
            { 47620, "BNV: Error executing command - REPAIR" },
            { 47621, "BNV: Error executing command - REFUND" },
            { 47622, "BNV: Cash box ready" },
            { 47624, "BNV: No operation data" },
            { 47626, "BNV: Invalid serial number of cashbox" },
            { 47627, "BNV: File access error" },
            { 47628, "BNV: Key of cash box moved" },
            { 47630, "Missing .> in ENC sequence" },
            { 47633, "BNV WBA error: Jamming in acceptor" },
            { 47634, "BNV WBA error: Jamming in stacker" },
            { 47635, "BNV WBA error: Paused error" },
            { 47636, "BNV WBA error: Cheated" },
            { 47637, "BNV WBA error: Stack motor failure" },
            { 47638, "BNV WBA error: Transport motor speed failure" },
            { 47639, "BNV WBA error: Transport motor failure" },
            { 47640, "BNV WBA error: Cash box not ready" },
            { 47641, "BNV WBA error: Validator head remove" },
            { 47642, "BNV WBA error: Boot ROM failure" },
            { 47643, "BNV WBA error: External ROM failure" },
            { 47644, "BNV WBA error: ROM failure" },
            { 47645, "BNV WBA error: External ROM failure writing failure" },
            { 47646, "BNV WBA error: Communication error" },
            { 47647, "Less OP-data from payment" },
            { 47648, "BNV: information without further specification" },
            { 47649, "Inlet left barrier current too low" },
            { 47651, "Inlet right barrier current too low" },
            { 47653, "Inlet length barrier current too low" },
            { 47655, "Cash box missing" },
            { 47656, "Cash box full or detector faulty" },
            { 47660, "Inlet flap detector does not lift up" },
            { 47662, "Outlet flap detector does not lift up" },
            { 47663, "Encashment diverter faulty" },
            { 47664, "Encashment diverter does not lift up" },
            { 47665, "Encashment detector faulty" },
            { 47666, "Encashment detector does not lift up" },
            { 47667, "Escrow diverter faulty" },
            { 47668, "Escrow diverter does not lift up" },
            { 47670, "Escrow diverter end of travel does not lift up" },
            { 47671, "Drum detector faulty" },
            { 47672, "Drum detector drum does not run" },
            { 47673, "Hardware not compatible" },
            { 47677, "Battery voltage faulty" },
            { 47685, "Green LED 2 current too low" },
            { 47687, "Red LED 1 current too low" },
            { 47696, "Green LED 1 current too high" },
            { 47698, "Green LED 2 current too high" },
            { 47699, "Red LED 1 current too low" },
            { 47700, "Red LED 1 current too high" },
            { 47708, "Green LED 1 current too high" },
            { 47709, "Green LED 2 current too low" },
            { 47716, "IR-LED 1 - high current" },
            { 47720, "Green LED 1 current too high" },
            { 47721, "Green LED 2 current too low" },
            { 47726, "Red LED 2 current too high" },
            { 47727, "Infrared LED 1 current too low" },
            { 47732, "Transport speed too slow" },
            { 47733, "Detector faulty" },
            { 47734, "Piston blocked" },
            { 47740, "Warning: Cashbox serial number faulty!" },
            { 47744, "BND: Cheated" },
            { 47745, "BND: Jammed" },
            { 47746, "BND: Disconnected" },
            { 47747, "BND: Connected" },
            { 47748, "BND: Download completed" },
            { 47749, "BND: Download failed" },
            { 47793, "Error to receive bills: jamming at sensor S17" },
            { 47794, "Error to receive bills: bill doesn't reach S17" },
            { 47816, "Error 200 at BSN385" },
            { 47856, "BNV blocked because of paper jam" },
            { 47872, "Credit system is OK" },
            { 48128, "Data access error ZCGVA" },
            { 48129, "CRC error in the operational data range ZCGVA ??" },
            { 48130, "Invalid operational data ZCGVA ??" },
            { 48147, "Unpermitted order to transaction recording ZCGVA ??" },
            { 48148, "Access error with full data file????" },
            { 48384, "BND: Few banknotes" },
            { 48385, "BND: Distribution low end" },
            { 48386, "BND: Error end" },
            { 48896, "BND: OP-DATA changed" },
            { 48897, "BND: Not enough notes" },
            { 48900, "BND: Out of operation" },
            { 48901, "BND: Ready again" },
            { 48904, "BND: Distribution low end" },
            { 48905, "BND: (Value-Set-Error) Vaults not closed" },
            { 48906, "BND: (Open-Error) Value of vaults not set" },
            { 48907, "BND: Cas. not on right position" },
            { 48908, "BND: Too many notes to be dispensed" },
            { 48909, "BND: Powerfail detected" },
            { 48910, "BND: Cassette disabled" },
            { 48911, "BND: Cassette enabled" },
            { 48912, "BND: Hardware Error" },
            { 48945, "BND: Banknotes low" },
            { 48946, "BND: Cassette is empty" },
            { 49144, "BND: Cassette door opened powerless" },
            { 49145, "BND: Cassette door closed w/o voltage" },
            { 49146, "BND: Cassette door opened" },
            { 49147, "BND: Cassette door closed" },
            { 51713, "Event 0xCA01/51713" },
            { 51745, "Barrier by input" },
            { 51777, "Lock barrier" },
            { 51809, "Unlock barrier" },
            { 51968, "Open barrier via input" },
            { 51969, "Open barrier via input" },
            { 51970, "Open barrier via input" },
            { 51971, "Open barrier via input" },
            { 51986, "Ticket request via input" },
            { 52016, "Confirm button pressed" },
            { 52017, "Cancel button pressed" },
            { 52994, "Service commands 02 at PGL (Basis CF02)" },
            { 52995, "Service commands 03 at PGL (Basis CF03)" },
            { 53015, "Event 0xCF17/53015" },
            { 53016, "Event 0xCF18/53016" },
            { 53017, "Event 0xCF19/53017" },
            { 53025, "Manual open at device" },
            { 53026, "Manual close at device" },
            { 53027, "Locked at device" },
            { 53028, "Unlocked at device" },
            { 53040, "Event 0xCF30/53040" },
            { 53041, "Event 0xCF31/53041" },
            { 53043, "Event 0xCF33/53043" },
            { 53044, "Service command 34 at PGL (Basis CF34)" },
            { 53056, "Service command 40 at PGL (Basis CF40)" },
            { 53057, "Service command 41 at PGL (Basis CF41)" },
            { 53058, "Service command 42 at PGL (Basis CF42)" },
            { 53059, "Service command 43 at PGL (Basis CF43)" },
            { 53060, "Service command 44 at PGL (Basis CF44)" },
            { 53090, "Event 0xCF62/53090" },
            { 53091, "Event 0xCF63/53091" },
            { 53092, "Event 0xCF64/53092" },
            { 53093, "Event 0xCF65/53093" },
            { 53094, "Event 0xCF66/53094" },
            { 53095, "Event 0xCF67/53095" },
            { 53096, "Event 0xCF68/53096" },
            { 53097, "Control command for new standard equipment" },
            { 53111, "Service command 77 at PGL (Basis CF77)" },
            { 53129, "Service command 89 at PGL (Basis CF89)" },
            { 53139, "Service command 93 at PGL (Basis CF93)" },
            { 53141, "Service command 95 at PGL (Basis CF95)" },
            { 53142, "Service command 96 at PGL (Basis CF96)" },
            { 53143, "Service command 97 at PGL (Basis CF97)" },
            { 53144, "Service command 98 at PGL (Basis CF98)" },
            { 55092, "Error in money processing" },
            { 55172, "Error in money processing" },
            { 55328, "Error in money processing" },
            { 55436, "Error in money processing" },
            { 55476, "Error in money processing" },
            { 55668, "Error in money processing" },
            { 56652, "Error in money processing" },
            { 56684, "Error in money processing" },
            { 56812, "Error in money processing" },
            { 57172, "Error in money processing" },
            { 57244, "Error in money processing" },
            { 57856, "Computing power (CPU) has passed a critical warning level" },
            { 57857, "System memory has passed a critical warning level" },
            { 57872, "New database error found" },
            { 57888, "entervo Analytics reports a problem with data extraction" },
            { 57889, "entervo Analytics reports a problem with data upload (transfer)" },
            { 57890, "entervo Analytics reports a problem with data deletion after upload" },
            { 57891, "entervo Analytics reports filesize too small for data file" },
            { 57892, "entervo Analytics reports a problem with the data load into the system (ETL)" },
            { 57893, "entervo Analytics reports a problem with the data backup (ETL)" },
            { 57894, "entervo Analytics reports a problem with the data rollback (ETL)" },
            { 57895, "entervo Analytics reports a problem with the data transfer (FTP)" },
            { 57896, "entervo Analytics reports a problem with data staging (ETL)" },
            { 57897, "entervo Analytics reports a problem with data aggregation (ETL)" },
            { 57898, "entervo Analytics reports a problem with the cube generation (ETL)" },
            { 57899, "entervo Analytics database file has reached a critical size" },
            { 57900, "entervo Analytics database file has reached a critical size" },
            { 57901, "entervo Analytics database file has reached a critical size" },
            { 57902, "entervo Analytics database file has reached a critical size" },
            { 57904, "Maximum capacity of database online backup is reached" },
            { 61696, "Invalid shift ID" },
            { 61703, "Database access error" },
            { 61706, "Field shift server: Fifo full" },
            { 61707, "Fifo locked by an (other) command" },
            { 65535, "Device in operation" },
        };
    }
}