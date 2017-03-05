﻿using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;
using WPILib.Commands;
using WPILib.LiveWindow;
using WPILib.SmartDashboard;
using ShibeBot.Subsystems;
using ShibeBot.Commands;
using ShibeBot.Subsystems.DriveTrain;
using ShibeBot.Subsystems.Pneumatics;
using ShibeBot.Subsystems.Thrower;

namespace ShibeBot
{
    public class ShibeBot : IterativeRobot
    {
        public static Oi Oi;

        public static DriveTrain DriveTrain = new DriveTrain();

        public static Pneumatics Pnuematics = new Pneumatics();

        public override void RobotInit()
        {
            Oi = new Oi();
        }

        public override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void AutonomousInit()
        {
        }

        public override void AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void TeleopInit()
        {
        }

        public override void DisabledInit()
        {

        }

        public override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
            Joystick stick = Oi.Pilot;
            //train._drive.ArcadeDrive(1, 1);

            stick.SetRumble(RumbleType.LeftRumble, stick.GetRawAxis(XboxMap.LeftTrigger));
            stick.SetRumble(RumbleType.RightRumble, stick.GetRawAxis(XboxMap.RightTrigger));

            SmartDashboard.PutNumber("POV", Oi.Pilot.GetPOV(0));
        }

        public override void TestPeriodic()
        {
            LiveWindow.Run();
        }
    }
}
