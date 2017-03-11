﻿using System;
using CSCore;
using OpenCvSharp;
using NetworkTables;
using WPILib;
using WPILib.Commands;
using WPILib.LiveWindow;
using WPILib.SmartDashboard;
using ShibeBot.Subsystems;
using ShibeBot.Commands;
using CTRE;
using ShibeBot.Subsystems.DriveTrain;
using ShibeBot.Subsystems.Pneumatics;
using ShibeBot.Subsystems.Thrower;
using ShibeBot.Subsystems.Tower;
using ShibeBot.Subsystems.Reporting;
<<<<<<< HEAD
using System.Threading;
using System.Collections.Generic;
=======
using ShibeBot.Subsystems.Lifter;
>>>>>>> master

namespace ShibeBot
{
    public class ShibeBot : IterativeRobot
    {
        public static Oi Oi;

        private const string CameraAddress = "";

        public static DriveTrain DriveTrain = new DriveTrain();
        public static Pneumatics Pnuematics = new Pneumatics();
        public static Thrower Thrower = new Thrower();
		public static Collector Collector = new Collector();
		public static Lifter Lifter = new Lifter();

        public static CameraServer CameraServer = CameraServer.Instance;

        //Reporting Subsystems (Requires Update!)
        public static Air Air = new Air();
        public static Match Match = new Match();
        public static Power Power = new Power();

        public static Camera Camera = new Camera();

		Command autonomousCommand;
		SendableChooser chooser;

        public override void RobotInit()
        {
            Oi = new Oi();
            Thread CameraThread = new Thread(() =>
			{
				AxisCamera camera = CameraServer.Instance.AddAxisCamera(CameraAddress);
				camera.SetResolution(640, 480);

				CvSink CvSink = CameraServer.Instance.GetVideo();
				CvSource CvSource = CameraServer.Instance.PutVideo("Blur", 640, 480);
				Mat source = new Mat();

				while (true)
				{

					if (CvSink.GrabFrame(source) == 0)
					{
						CvSource.NotifyError(CvSink.GetError());
						continue;
					}
					CvSource.PutFrame(source);
	            }

            });
            CameraThread.IsBackground = true;
            CameraThread.Start();
        }

        public override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void AutonomousInit()
        {
			autonomousCommand = new AutonomousCommand();

			if (autonomousCommand != null) autonomousCommand.Start();
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

        private double flot = 0;
        public override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
            Joystick stick = Oi.Pilot;
            //train._drive.ArcadeDrive(1, 1);

            stick.SetRumble(RumbleType.LeftRumble, stick.GetRawAxis(XboxMap.LeftTrigger));
            stick.SetRumble(RumbleType.RightRumble, stick.GetRawAxis(XboxMap.RightTrigger));

            SmartDashboard.PutNumber("POV", Oi.Pilot.GetPOV(0));
			SmartDashboard.PutString("Drive Mode", Oi.DriveStyle.ToString());
			SmartDashboard.PutString("Gear", Oi.ShifterGear.ToString());

            SmartDashboard.PutData("Compressor On", new CompressorOnCommand());
            SmartDashboard.PutData("Compressor Off", new CompressorOffCommand());

            Air.Update();
            Match.Update();
            Power.Update();

<<<<<<< HEAD

            flot += 0.05;
=======
>>>>>>> master
        }

        public override void TestPeriodic()
        {
            LiveWindow.Run();
        }
    }
}
