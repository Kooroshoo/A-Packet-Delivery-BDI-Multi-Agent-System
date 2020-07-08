:- consult("UnityLogic/KBs/UnityLogicAgentAPI.prolog").

add take_box(B) && (\+ belief jobDone(B)) => [
	check_artifact_belief(B, s_dir(S_Dir)),
	check_artifact_belief(B, d_dir(D_Dir)),
	act (getExchangeArea(S_Dir), SArea),
	act (getExchangeArea(D_Dir), DArea),
	cr goto(SArea),
	act pickup(B),
	cr goto(DArea),
	cr dropDown(DArea),
	act (getRailBot(D_Dir), RailBot),
	add_agent_desire(RailBot, deliver_box(B)),
	act (getChargingStation, Base),
	cr goto(Base),
	add_belief(jobDone(B)),

	stop
].

