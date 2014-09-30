using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum EnumSectionType
{
    Billboard = 1,
    Committee = 2,
    SisterIllness = 3,
    ChapterDirectives = 4,
    HelpFAQ = 5,
    DeathNotice = 6,
    Chapter2008 = 7,
    ACCommon = 8,
    Election2008 = 9,
    Private = 10,
    AreaChapter = 12,
    CheckThisOut = 13,
    Job = 14,
    Department = 15,
    Location = 16,
    News = 17,
    Calender = 18,
    Directory = 19,
    BulletinBoard = 20,
    Email = 21,
    Discusstion = 22,
    Reference = 23,
    CommunityNews =24,
    Help=25,
    Link=26,
    HOTP = 27, 
    EmailIspMgt=28,
    ContactList = 29,
    Prayer = 30

};


public enum EnumSystemSettings
{
BadPasswordLockout = 1,
EmailPasswordResets = 2,
GraceLogins = 3

};

public enum HomePageColumnTypeEnum
{
    LeftColumn = 1,
    MiddleColumn = 2,
    RightColumn = 3

};