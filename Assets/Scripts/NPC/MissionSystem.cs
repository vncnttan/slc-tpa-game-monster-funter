using UnityEngine;
using System.Collections.Generic;

public class MissionSystem {
    private static MissionSystem activeMission;
    private MissionSystem(){
        AllMission.Add("Go and Talk to Lyra");
        AllMission.Add("Use Basic Attack 10 times");
        AllMission.Add("Use All Skills Available");
        AllMission.Add("Talk to All People in Village");
        AllMission.Add("Go and Conquer the Maze");
        AllProgress.Add(1);
        AllProgress.Add(10);
        AllProgress.Add(2);
        AllProgress.Add(3);
        AllProgress.Add(1);
        missionTracker = 0;
        currentProgress = 0;
    }
    private static int currentProgress;
    private static int missionTracker;
    private static List<string> AllMission = new List<string>();
    private static List<int> AllProgress = new List<int>();

    public static MissionSystem GetMission(){
        if(activeMission == null){
            activeMission = new MissionSystem();
        }
        return activeMission;
    }
    public int GetMissionTracker(){
        return missionTracker;
    }
    public void ProgressMission(){
        missionTracker++;
    }

    public void AddProgress(){
        currentProgress++;
    }

    public string GetMissionDesc(){
        return AllMission[missionTracker];
    }
    public int GetCurrentProgress(){
        return currentProgress;
    }
    public int GetCurrentGoalProgress(){
        return AllProgress[missionTracker];
    }

    public bool isMissionCompleted(){
        if(currentProgress >= AllProgress[missionTracker]){
            return true;
        } else {
            return false;
        }
    }

    public void ProgressToNextMission(){
        currentProgress = 0;
        missionTracker++;
    }
}