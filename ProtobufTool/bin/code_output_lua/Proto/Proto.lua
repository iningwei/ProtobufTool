---@class Proto
local Proto={
Schema=[[
syntax="proto3"
message C2SRoomLogin{
string roomId = 1;
string version = 2;
string token = 3;
}

// 100指令错误,101成功，102无权限，103任务开启失败
message S2CRoomLogin{
int32 result = 1;
string blueCompName=2;
string redCompName=3;
}


//直播间评论数据推送
// S2C_MsgID=1000
message S2CLiveComment{
repeated PbLiveComment msg = 1;
}

message PbLiveComment{
string secOpenId = 1;  //评论用户的加密openid, 当前其实没有加密
string content = 2;   // 评论内容
string avatarUrl = 3;  // 评论用户头像
string nickname = 4;   // 评论用户昵称(不加密)
int64 timeStamp = 5;   // 评论毫秒级时间戳
}

//直播间礼物数据推送
// S2C_MsgID=1001
message S2CLiveGift{
repeated PbLiveGift msg = 1;
}

message PbLiveGift{
string secOpenId = 1;  //评论用户的加密openid, 当前其实没有加密
string secGiftId = 2;   // 加密的礼物id
uint32 giftNum = 3;   // 送出的礼物数量
uint32 giftValue = 4;   // 礼物总价值，单位分
string avatarUrl = 5;  // 用户头像
string nickname = 6;   // 用户昵称(不加密)
int64 timeStamp = 7;   // 礼物毫秒级时间戳
}

//直播间点赞数据推送
// S2C_MsgID=1002
message S2CLiveLike{
repeated PbLiveLike msg = 1;
}

message PbLiveLike{
string secOpenId = 1;  //评论用户的加密openid, 当前其实没有加密
uint32 likeNum = 2;   // 点赞数量，上游2s合并一次数据
string avatarUrl = 3;  // 点赞用户头像
string nickname = 4;   // 点赞用户昵称(不加密)
int64 timeStamp = 5;   // 点赞毫秒级时间戳
}



//玩家分数上传
// C2S_MsgID=2000
message C2SUserScore{
repeated PbUserScore userScore = 1;
}

message PbUserScore{
string uid = 1;//玩家uid
int32 score = 2;//玩家分数
int32 camp = 3;//玩家阵营
}

//玩家排名推送
// S2C_MsgID=2001
message S2CUserScoreSeason{
repeated PbUserRanking wordRankingList = 1;
repeated PbUserRanking curRankingList = 2;
}

message PbUserRanking{
string uid = 1;//玩家uid
int32 rankingNum = 2;//玩家排名
int32 camp = 3;//玩家阵营
int32 score = 4;//玩家分数
PbUserBase userinfo = 5;//玩家基础数据
}

message PbUserBase{
string secOpenId = 1;
string avatarUrl = 2;
string nickname = 3;
}




//心跳
//C2S_MsgID=10000
message C2SPing{
int32 index=1;
int64 timeStamp=2;//时间戳，精确到毫秒
}

//服务端下行时把客户端的index传过来
//S2C_MsgID=10001
message S2CPing{
int32 index=1;
int64 timeStamp=2;//时间戳，精确到毫秒
}

]]
}
return Proto
