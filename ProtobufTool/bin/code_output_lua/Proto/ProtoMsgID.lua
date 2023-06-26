---@type ProtoMsgIDExt
ProtoMsgIDExt=require "ProtoMsgIDExt"

--发送
Proto.MsgIdByNameCS = {
["C2SRoomLogin"]="100",

--玩家分数上传
["C2SUserScore"]="2000",

--心跳
["C2SPing"]="10000",

}

--接收
Proto.MsgIdByNameSC = {
-- 100指令错误,101成功，102无权限，103任务开启失败
["S2CRoomLogin"]="101",

--直播间评论数据推送
["S2CLiveComment"]="1000",

--直播间礼物数据推送
["S2CLiveGift"]="1001",

--直播间点赞数据推送
["S2CLiveLike"]="1002",

--玩家排名推送
["S2CUserScoreSeason"]="2001",

--服务端下行时把客户端的index传过来
["S2CPing"]="10001",

}

Proto.MsgNameByIdCS={}
for k, v in pairs(Proto.MsgIdByNameCS) do
    Proto.MsgNameByIdCS[v] = k
end

Proto.MsgNameByIdSC = {}
for k, v in pairs(Proto.MsgIdByNameSC) do
    Proto.MsgNameByIdSC[v] = k
end

setmetatable(Proto.MsgIdByNameCS,{
    __index=function(t,k)
        local value=ProtoMsgIDExt.MsgIdByNameCS[k]
        if value~=nil then
            t[k]=value
            return value
        end
    end
})

setmetatable(Proto.MsgIdByNameSC,{
    __index=function(t,k)
        local value=ProtoMsgIDExt.MsgIdByNameSC[k]
        if value~=nil then
            t[k]=value
            return value
        end
    end
})

setmetatable(Proto.MsgNameByIdCS,{
    __index=function(t,k)
        local value=ProtoMsgIDExt.MsgNameByIdCS[k]
        if value~=nil then
            t[k]=value
            return value
        end
    end
})

setmetatable(Proto.MsgNameByIdSC,{
    __index=function(t,k)
        local value=ProtoMsgIDExt.MsgNameByIdSC[k]
        if value~=nil then
            t[k]=value
            return value
        end
    end
})

