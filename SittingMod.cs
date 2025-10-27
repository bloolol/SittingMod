using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace SittingMod
{
    public class Main : Script
    {
        public static int SittingTaskScriptStatus = -1;
        private List<Model> SeatModels = new List<Model>()
    {
      (Model) (-1631057904),
      (Model) (-171943901),
      (Model) 1805980844,
      (Model)(-403891623),
      (Model) 437354449,
      (Model) 291348133,
      (Model) 146905321,
      (Model)(-741944541),
      (Model) (-628719744),
      (Model)(-1062810675),
      (Model)(-1215681419),
      (Model)(2037887057),
      (Model)(-763859088),
      (Model)(-1317098115),
      (Model)(-71417349),
      (Model)(1290593659),
      (Model)(-70627249),
      (Model)(307625467),
      (Model)(-1838046182),
      (Model)(1544445682),
      (Model)(-1461986002),
      (Model)(39200959),
      (Model)(836224685),
      (Model)(-157036474),
      (Model)(376180694),
      (Model)(-373650829),
      (Model)(-67162372),
      (Model)(-1063831511),
      (Model)(1285701428),
      (Model)(1623746432),
      (Model)(167066071),
      (Model)(28672923),
      (Model)(-293380809),
      (Model)(-591349326),
      (Model)(1355718178),
      (Model)(-294499241),
      (Model)(-1320300017),
      (Model)(-1005619310),
      (Model)(-1175488618),
      (Model)(1787607276),
      (Model)(-1761659350),
      (Model)(-1498352975),
      (Model)(-2024837020),
      (Model)(2017293393),
      (Model)(2030873292),
      (Model)(1523123209),
      (Model)(47332588),
      (Model)(525667351),
      (Model)(764848282),     
      (Model)(867556671),
      (Model)(1037469683),
      (Model)(1593135630),
      (Model)(1181479993),
      (Model)(-99500382),
      (Model)(-1190156817),
      (Model)(-425962029),
      (Model)(2040839490),
      (Model)(1805980844)




    };
        private int SittingAnimIndex;
        private Vector3 SeatEntryCoords;
        private float SitAttachOffsetZ;
        private float SitAttachOffsetY = 0.0f;
        private float SitAttachOffsetX;
        private Prop Seat;

        public Main() => this.Tick += new EventHandler(this.OnTick);

        private void OnTick(object sender, EventArgs e)
        {
            if (Game.IsPaused)
                return;
            Prop[] nearbyProps = World.GetNearbyProps(Game.Player.Character.Position, 25f);
            for (int index = 0; index < nearbyProps.Length; ++index)
            {
                if (Main.SittingTaskScriptStatus == -1 && !Game.Player.Character.IsInVehicle() && Game.Player.WantedLevel == 0 && this.SeatModels.Contains(nearbyProps[index].Model) && (double)Game.Player.Character.Position.DistanceTo(nearbyProps[index].Position) <= 1.4)
                {
                    Utils.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to sit down.");
                    if (Game.IsControlJustPressed(Control.Context))
                    {
                        this.Seat = nearbyProps[index];
                        Main.SittingTaskScriptStatus = 0;
                    }
                }
            }
            switch (Main.SittingTaskScriptStatus)
            {
                case 0:
                    this.SeatEntryCoords = this.Seat.Position + this.Seat.ForwardVector * -0.9f;
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, new InputArgument[8]
                    {
            (InputArgument) Game.Player.Character,
            (InputArgument) this.SeatEntryCoords.X,
            (InputArgument) this.SeatEntryCoords.Y,
            (InputArgument) this.SeatEntryCoords.Z,
            (InputArgument) 1.0,
            (InputArgument) 20000,
            (InputArgument) (this.Seat.Heading - 180f),
            (InputArgument) 0.1
                    });
                    Main.SittingTaskScriptStatus = 1;
                    break;
                case 1:
                    if (Function.Call<bool>(Hash.IS_ENTITY_AT_COORD, new InputArgument[10]
                    {
            (InputArgument) Game.Player.Character,
            (InputArgument) this.SeatEntryCoords.X,
            (InputArgument) this.SeatEntryCoords.Y,
            (InputArgument) this.SeatEntryCoords.Z,
            (InputArgument) 2.0,
            (InputArgument) 2.0,
            (InputArgument) 2.0,
            (InputArgument) 0,
            (InputArgument) 1,
            (InputArgument) 0
                    }))
                    {
                        Script.Wait(2000);
                        Game.Player.Character.Heading = this.Seat.Heading - 180f;
                        Game.Player.Character.TaskPlayAnim("amb@prop_human_seat_chair@male@generic@enter", "enter_forward", -1);
                        Main.SittingTaskScriptStatus = 2;
                        break;
                    }
                    break;
                case 2:
                    switch (this.Seat.Model.Hash.ToString())
                    {
                        case "-1631057904":
                            this.SitAttachOffsetZ = 0.48f;
                            break;
                        case "-171943901":
                            this.SitAttachOffsetZ = 0.04f;
                            break;
                        case "-403891623": //prop_bench_08
                            this.SitAttachOffsetZ = 0.5f;


                            this.SitAttachOffsetX = 0.5f;
                            break;
                        case "-741944541":
                            this.SitAttachOffsetZ = 0.5f;
                            break;
                        case "146905321":
                            this.SitAttachOffsetZ = 0.5f;
                            break;
                        case "1805980844":
                            this.SitAttachOffsetZ = 0.5f;
                            break;
                        case "291348133":
                            this.SitAttachOffsetZ = 0.05f;
                            break;
                        case "437354449":
                            this.SitAttachOffsetZ = 0.5f;
                            break;
                        case "-1190156817":
                            this.SitAttachOffsetZ = 0.9f;
                            this.SitAttachOffsetY = .2f;
                            this.SitAttachOffsetX = 0.0f;
                            break;// stool kitchen
                        case "2040839490":
                            this.SitAttachOffsetZ = 0.0f;
                            this.SitAttachOffsetY = 0.0f;
                            this.SitAttachOffsetX = 0.0f;
                            break;
                        default:
                            this.SitAttachOffsetZ = 0.2f;
                            this.SitAttachOffsetY = 0.0f;
                            this.SitAttachOffsetX = 0.0f;
                       
                            break;
                    }
                    Script.Wait(1000);
                    Game.Player.Character.AttachTo((Entity)this.Seat, new Vector3(this.SitAttachOffsetX, SitAttachOffsetY, this.SitAttachOffsetZ), new Vector3(0.0f, 0.0f, -180f));
                    Main.SittingTaskScriptStatus = 3;
                    break;
                case 3:
                    Script.Wait(500);
                    Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@generic@base", "base", -1);
                    Main.SittingTaskScriptStatus = 4;
                    break;
            }
            if (Main.SittingTaskScriptStatus != 4)
                return;
            Utils.DisplayHelpTextThisFrame("Press ~INPUT_COVER~ to cycle animations.~n~Press ~INPUT_CONTEXT~ to get up.");
            if (Game.IsControlJustPressed(Control.Context))
            {
                Game.Player.Character.TaskPlayAnim("amb@prop_human_seat_chair@male@generic@exit", "exit_forward", -1);
                Script.Wait(800);
                Game.Player.Character.Detach();
                this.SittingAnimIndex = 0;
                Main.SittingTaskScriptStatus = -1;
            }
            if (Game.IsControlJustPressed(Control.Cover))
            {
                ++this.SittingAnimIndex;
                if (this.SittingAnimIndex == 5)
                    this.SittingAnimIndex = 0;
                switch (this.SittingAnimIndex)
                {
                    case 0:
                        Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@generic@base", "base", -1);
                        break;
                    case 1:
                        Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@left_elbow_on_knee@base", "base", -1);
                        break;
                    case 2:
                        Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@right_foot_out@base", "base", -1);
                        break;
                    case 3:
                        Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@recline_b@base_b", "base_b", -1);
                        break;
                    case 4:
                        Game.Player.Character.TaskPlayAnimLoop("amb@prop_human_seat_chair@male@elbows_on_knees@base", "base", -1);
                        break;
                }
            }
            if (!Game.Player.IsDead && !Game.Player.Character.IsRagdoll)
                return;
            Game.Player.Character.Detach();
            this.SittingAnimIndex = 0;
            Main.SittingTaskScriptStatus = -1;
        }
    }

    public static class Utils
    {
        public static void DisplayHelpTextThisFrame(string text)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, new InputArgument[1]
            {
        (InputArgument) "STRING"
            });
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, new InputArgument[1]
            {
        (InputArgument) text
            });
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_HELP, new InputArgument[4]
            {
        (InputArgument) 0,
        (InputArgument) 0,
        (InputArgument) 1,
        (InputArgument)(-1)
            });
        }

        public static void TaskPlayAnim(this Ped ped, string animDict, string animFile, int duration)
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, new InputArgument[1]
            {
        (InputArgument) animDict
            });
            DateTime dateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1000);
            do
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, new InputArgument[1]
                {
          (InputArgument) animDict
                }))
                    Script.Yield();
                else
                    goto label_4;
            }
            while (!(DateTime.Now >= dateTime));
            return;
        label_4:
            Function.Call(Hash.TASK_PLAY_ANIM, new InputArgument[11]
            {
        (InputArgument) ped,
        (InputArgument) animDict,
        (InputArgument) animFile,
        (InputArgument) 8f,
        (InputArgument)(-4f),
        (InputArgument) duration,
        (InputArgument) 8,
        (InputArgument) 0.0f,
        (InputArgument) false,
        (InputArgument) false,
        (InputArgument) false
            });
        }

        public static void TaskPlayAnimLoop(
          this Ped ped,
          string animDict,
          string animFile,
          int duration)
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, new InputArgument[1]
            {
        (InputArgument) animDict
            });
            DateTime dateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1000);
            do
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, new InputArgument[1]
                {
          (InputArgument) animDict
                }))
                    Script.Yield();
                else
                    goto label_4;
            }
            while (!(DateTime.Now >= dateTime));
            return;
        label_4:
            Function.Call(Hash.TASK_PLAY_ANIM, new InputArgument[11]
            {
        (InputArgument) ped,
        (InputArgument) animDict,
        (InputArgument) animFile,
        (InputArgument) 8f,
        (InputArgument)(-4f),
        (InputArgument) duration,
        (InputArgument) 9,
        (InputArgument) 0.0f,
        (InputArgument) false,
        (InputArgument) false,
        (InputArgument) false
            });
        }

        public static void TaskPlayAnimUpperBody(
          this Ped ped,
          string animDict,
          string animFile,
          int duration,
          bool loop)
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, new InputArgument[1]
            {
        (InputArgument) animDict
            });
            DateTime dateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1000);
            do
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, new InputArgument[1]
                {
          (InputArgument) animDict
                }))
                    Script.Yield();
                else
                    goto label_4;
            }
            while (!(DateTime.Now >= dateTime));
            return;
        label_4:
            if (!loop)
                Function.Call(Hash.TASK_PLAY_ANIM, new InputArgument[11]
                {
          (InputArgument) ped,
          (InputArgument) animDict,
          (InputArgument) animFile,
          (InputArgument) 8f,
          (InputArgument)(-4f),
          (InputArgument) duration,
          (InputArgument) 48,
          (InputArgument) 0.0f,
          (InputArgument) false,
          (InputArgument) false,
          (InputArgument) false
                });
            else
                Function.Call(Hash.TASK_PLAY_ANIM, new InputArgument[11]
                {
          (InputArgument) ped,
          (InputArgument) animDict,
          (InputArgument) animFile,
          (InputArgument) 8f,
          (InputArgument)(-4f),
          (InputArgument) duration,
          (InputArgument) 49,
          (InputArgument) 0.0f,
          (InputArgument) false,
          (InputArgument) false,
          (InputArgument) false
                });
        }

        public static double GetEntityAnimCurrentTime(this Ped ped, string animDict, string animFile)
        {
            return Function.Call<double>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, new InputArgument[3]
            {
        (InputArgument) ped,
        (InputArgument) animDict,
        (InputArgument) animFile
            });
        }

        public static int GetRandomIntInRange(int min, int max) => new Random().Next(min, max + 1);
    }
}
