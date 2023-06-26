---@class C2SRoomLogin
---@field public roomId string
---@field public version string
---@field public token string
local C2SRoomLogin = {}

---@class S2CRoomLogin
---@field public result number
---@field public blueCompName string
---@field public redCompName string
local S2CRoomLogin = {}

---@class S2CLiveComment
---@field public msg PbLiveComment[]
local S2CLiveComment = {}

---@class PbLiveComment
---@field public secOpenId string @评论用户的加密openid, 当前其实没有加密
---@field public content string @ 评论内容
---@field public avatarUrl string @ 评论用户头像
---@field public nickname string @ 评论用户昵称(不加密)
---@field public timeStamp number @ 评论毫秒级时间戳
local PbLiveComment = {}

---@class S2CLiveGift
---@field public msg PbLiveGift[]
local S2CLiveGift = {}

---@class PbLiveGift
---@field public secOpenId string @评论用户的加密openid, 当前其实没有加密
---@field public secGiftId string @ 加密的礼物id
---@field public giftNum number @ 送出的礼物数量
---@field public giftValue number @ 礼物总价值，单位分
---@field public avatarUrl string @ 用户头像
---@field public nickname string @ 用户昵称(不加密)
---@field public timeStamp number @ 礼物毫秒级时间戳
local PbLiveGift = {}

---@class S2CLiveLike
---@field public msg PbLiveLike[]
local S2CLiveLike = {}

---@class PbLiveLike
---@field public secOpenId string @评论用户的加密openid, 当前其实没有加密
---@field public likeNum number @ 点赞数量，上游2s合并一次数据
---@field public avatarUrl string @ 点赞用户头像
---@field public nickname string @ 点赞用户昵称(不加密)
---@field public timeStamp number @ 点赞毫秒级时间戳
local PbLiveLike = {}

---@class C2SUserScore
---@field public userScore PbUserScore[]
local C2SUserScore = {}

---@class PbUserScore
---@field public uid string @玩家uid
---@field public score number @玩家分数
---@field public camp number @玩家阵营
local PbUserScore = {}

---@class S2CUserScoreSeason
---@field public wordRankingList PbUserRanking[]
---@field public curRankingList PbUserRanking[]
local S2CUserScoreSeason = {}

---@class PbUserRanking
---@field public uid string @玩家uid
---@field public rankingNum number @玩家排名
---@field public camp number @玩家阵营
---@field public score number @玩家分数
---@field public userinfo PbUserBase @玩家基础数据
local PbUserRanking = {}

---@class PbUserBase
---@field public secOpenId string
---@field public avatarUrl string
---@field public nickname string
local PbUserBase = {}

---@class C2SPing
---@field public index number
---@field public timeStamp number @时间戳，精确到毫秒
local C2SPing = {}

---@class S2CPing
---@field public index number
---@field public timeStamp number @时间戳，精确到毫秒
local S2CPing = {}

