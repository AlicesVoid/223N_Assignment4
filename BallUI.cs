/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 11/05/2022
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;


public class RicochetBallInterface: Form
{

      // USER INTERFACE INITIALIZATION
      private Label title       = new Label();
      private Label coord_title = new Label();
      private Label speed_title = new Label();
      private Label dir_title   = new Label();
      private Label x_title     = new Label();
      private Label y_title     = new Label();
      private Panel header            = new Panel();
      private Panel controlz          = new Panel();
      private Graphicpanel ball_mover = new Graphicpanel();
      private Button init_button  = new Button();
      private Button start_button = new Button();
      private Button quit_button  = new Button();
      private TextBox speed_input = new TextBox();
      private TextBox dir_input   = new TextBox();
      private TextBox x_output    = new TextBox();
      private TextBox y_output    = new TextBox();

      //UI STYLE VARIABLES
      private Size program_size   = new Size(1000, 1200);
      private Size button_size    = new Size(150, 100);
      private Font control_font      = new Font("Comic Sans MS", 18, FontStyle.Regular);
      private Font title_font        = new Font("Impact", 25, FontStyle.Bold);

      //UX VARIABLES 
      private static double BALL_MOVEMENTS = 30;
      private static double delta_x = 0;
      private static double delta_y = 0; 
      private static double x_coord = 0; 
      private static double y_coord = 0; 

      private static double peace_of_mind_counter = 0;

      //Runtime State 
      private enum State {Init, Line, Pause};
      private static State runtime = State.Init;
      
      //Execution State
      private enum Execution_state {Executing, Waiting_to_terminate};           
      private Execution_state current_state = Execution_state.Executing;

      //Clock Systems
      private static System.Timers.Timer exit_clock = new System.Timers.Timer();  
      private static System.Timers.Timer ball_clock = new System.Timers.Timer();  
      private const double clock_time = 3.0; //Hz
      private const double one_second = 1000.0; //ms
      private const double ball_interval = one_second / clock_time;


      //CONSTRUCTOR
      public RicochetBallInterface() 
      {
            //DECLARE SIZES
            MaximumSize = program_size;
            MinimumSize = program_size;

            //INIT. TEXT
            title.Text        = "Ricochet Ball by Amelia Rotondo";
            coord_title.Text  = "Ball Coords:";
            speed_title.Text  = "SET THE SPEED:";
            dir_title.Text    = "SET THE ANGLE:";
            x_title.Text      = "X = ";
            y_title.Text      = "Y = ";
            init_button.Text  = "Init.";
            start_button.Text = "Start!";
            quit_button.Text  = "Quit...";
                                    
            //INIT. SIZES
            Size              = MaximumSize;
            title.Size        = new Size(800, 90);
            header.Size       = new Size(1000, 200);
            ball_mover.Size   = new Size(1000, 700);
            controlz.Size     = new Size(1000, 300);
            coord_title.Size  = new Size(200, 60);
            speed_title.Size  = new Size(200, 60);
            dir_title.Size    = new Size(200, 60);
            x_title.Size      = new Size(150, 70);
            y_title.Size      = new Size(150, 70);
            init_button.Size  = button_size;
            start_button.Size = button_size;
            quit_button.Size  = button_size;
            speed_input.Size  = new Size(120, 80);
            dir_input.Size    = new Size(120, 80);
            x_output.Size     = new Size(120, 90);
            y_output.Size     = new Size(120, 90);
            
            //INIT. COLORS
            header.BackColor       = Color.LightSeaGreen;
            ball_mover.BackColor   = Color.LightCyan;
            controlz.BackColor     = Color.LightGoldenrodYellow;
            coord_title.BackColor  = Color.LightCoral;
            speed_title.BackColor  = Color.LightCoral;
            dir_title.BackColor    = Color.LightCoral;
            x_title.BackColor      = Color.LightCoral;
            y_title.BackColor      = Color.LightCoral;
            speed_input.BackColor  = Color.LightPink;
            dir_input.BackColor    = Color.LightPink;
            x_output.BackColor     = Color.LightPink;
            y_output.BackColor     = Color.LightPink; 
            init_button.BackColor  = Color.LightPink;
            start_button.BackColor = Color.LightPink;
            quit_button.BackColor  = Color.LightPink;

            //INIT. FONTS
            title.Font        = title_font;
            coord_title.Font  = control_font;
            speed_title.Font  = control_font;
            dir_title.Font    = control_font; 
            x_title.Font      = control_font;
            y_title.Font      = control_font;
            init_button.Font  = control_font;
            start_button.Font = control_font;
            quit_button.Font  = control_font;
            speed_input.Font  = control_font;
            dir_input.Font    = control_font;
            x_output.Font     = control_font;
            y_output.Font     = control_font;

            //INIT. ALIGNMENTS
            title.TextAlign        = ContentAlignment.MiddleCenter;
            coord_title.TextAlign  = ContentAlignment.MiddleCenter;
            speed_title.TextAlign  = ContentAlignment.MiddleCenter;
            dir_title.TextAlign    = ContentAlignment.MiddleCenter;
            x_title.TextAlign      = ContentAlignment.MiddleCenter;
            y_title.TextAlign      = ContentAlignment.MiddleCenter;
            init_button.TextAlign  = ContentAlignment.MiddleCenter;
            start_button.TextAlign = ContentAlignment.MiddleCenter;
            quit_button.TextAlign  = ContentAlignment.MiddleCenter;

            //INIT. LOCATIONS
            header.Location     = new Point(0, 0);
            ball_mover.Location = new Point(0, 200);
            controlz.Location   = new Point(0, 900);

            title.Location       = new Point(125, 40);
            coord_title.Location = new Point(450, 130);
            speed_title.Location = new Point(200, 10);
            dir_title.Location   = new Point(550, 10);
            x_title.Location     = new Point(300, 200);
            y_title.Location     = new Point(600, 200);
            speed_input.Location = new Point(410, 10);
            dir_input.Location   = new Point(760, 10);
            x_output.Location    = new Point(460, 200);
            y_output.Location    = new Point(760, 200);

            init_button.Location  = new Point(30, 10);
            start_button.Location = new Point(40, 150);
            quit_button.Location = new Point(790, 75);

            //INIT. CONTROLS
            Controls.Add(header);
            Controls.Add(ball_mover);
            Controls.Add(controlz);
            header.Controls.Add(title);
                      
            //INIT. CONTROLZ
            controlz.Controls.Add(coord_title);
            controlz.Controls.Add(speed_title);
            controlz.Controls.Add(dir_title);
            controlz.Controls.Add(x_title);
            controlz.Controls.Add(y_title);
            controlz.Controls.Add(speed_input);
            controlz.Controls.Add(dir_input);
            controlz.Controls.Add(x_output);
            controlz.Controls.Add(y_output);
            controlz.Controls.Add(init_button);
            controlz.Controls.Add(start_button);
            controlz.Controls.Add(quit_button);  

            //INIT. EVENT HANDLERS
            //init_button += new EventHandler(resetrun);
            //start_button += new EventHandler(drawline);
            //quit_button += new EventHandler(stoprun);
            
            //INIT. CLOCK CONFIG
            exit_clock.Enabled = false;     //Clock is turned off at start program execution.
            exit_clock.Interval = 2500;     //7500ms = 7.5seconds.  Clock will tick at intervals of 7.5 seconds
            //exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

            ball_clock.Enabled = false;     //Clock is turned off at start program execution.
            ball_clock.Interval = ball_interval;     // 3.0 Hz
            //ball_clock.Elapsed += new ElapsedEventHandler(ballHelper);   //Attach a method to the clock.


            //Open this user interface window in the center of the display.
            CenterToScreen();

      }//End of constructor RicochetBallInterface
      

      /*
      protected void drawline(Object sender, EventArgs events)
      {
      switch(runtime)
      {
                  case State.Init:
                        Console.WriteLine("Starting Point Input = (" + x1_coordinput.Text + ", " + y1_coordinput.Text + ")");
                        Console.WriteLine("Midpoint Input = (" + x_mid.Text + ", " + y_mid.Text + ")");
                        Console.WriteLine("Finishing Point Input = (" + x2_coordinput.Text + ", " + y2_coordinput.Text + ")");

                        if(Convert.ToDouble(x1_coordinput.Text) < 0 || Convert.ToDouble(x1_coordinput.Text) > 1200)
                              {throw new Exception("Invalid Input - Input Out of Bounds");}
                        else if(Convert.ToDouble(x2_coordinput.Text) < 0 || Convert.ToDouble(x2_coordinput.Text) > 1200)
                              {throw new Exception("Invalid Input - Input Out of Bounds");}
                        else if(Convert.ToDouble(y1_coordinput.Text) < 0 || Convert.ToDouble(y2_coordinput.Text) > 560)
                              {throw new Exception("Invalid Input - Input Out of Bounds");}
                        else if(Convert.ToDouble(y_mid.Text) < 0 || Convert.ToDouble(y_mid.Text) > 560)
                              {throw new Exception("Invalid Input - Input Out of Bounds");}
                        else if(Convert.ToDouble(x_mid.Text) < 0 || Convert.ToDouble(x_mid.Text) > 1200)
                              {throw new Exception("Invalid Input - Input Out of Bounds");}
                                                            
                        x_coord = Convert.ToDouble(x1_coordinput.Text);
                        y_coord = Convert.ToDouble(y1_coordinput.Text);

                        
                        delta_x1 = ((Convert.ToDouble(x_mid.Text)) - x_coord) / BALL_MOVEMENTS;
                        delta_y1 = ((Convert.ToDouble(y_mid.Text)) - y_coord) / BALL_MOVEMENTS;
                        delta_x2 = ((Convert.ToDouble(x2_coordinput.Text)) - (Convert.ToDouble(x_mid.Text))) / BALL_MOVEMENTS;
                        delta_y2 = ((Convert.ToDouble(y2_coordinput.Text)) - (Convert.ToDouble(y_mid.Text))) / BALL_MOVEMENTS;
            
                        x_coord -= 10;
                        y_coord -= 10;

                        Console.WriteLine("Delta x1 = " + delta_x1 + ", Delta y1 = " + delta_y1);
                        Console.WriteLine("Delta x2 = " + delta_x2 + ", Delta y2 = " + delta_y2);

                        runtime = State.Line;
                        ball_clock.Interval= ball_interval;     // 3.0 Hz
                        ball_clock.Enabled = true;
                        gobutton.Text = "Pause?";
                        break;
                  
                  case State.Line: 
                        runtime = State.Pause;
                        ball_clock.Enabled = false;
                        gobutton.Text = "Go!!!";
                        
                  break;

                  default: 
                        runtime = State.Line;
                        ball_clock.Interval= ball_interval;     // 3.0 Hz
                        ball_clock.Enabled = true;
                        gobutton.Text = "Pause?";
                  break;
      }
            line_drawer.Refresh(); 
      }//End of drawline

      //Method to Exit and LEAVE the Program (waits 2.5 seconds before closing)
      protected void stoprun(Object sender, EventArgs events)
      {switch(current_state)
      {case Execution_state.Executing:
                  exit_clock.Interval= 2500;     //2500ms = 2.5 seconds
                  exit_clock.Enabled = true;
                  quitbutton.Text = "Are You Sure!?";
                  current_state = Execution_state.Waiting_to_terminate;
                  break;
      case Execution_state.Waiting_to_terminate:
                  exit_clock.Enabled = false;
                  quitbutton.Text = "Quit...";
                  current_state = Execution_state.Executing;
                  break;
      }//End of switch statement
      }//End of method stoprun.  In C Sharp language "method" means "function".

      protected void resetrun(Object sender, EventArgs events)
            {
                  switch(runtime)
                  {
                        case State.Init:
                              break;

                        default:
                              runtime = State.Init;

                              x1_coordinput.Text = " ";
                              x_mid.Text = " ";
                              x2_coordinput.Text = " ";
                              y1_coordinput.Text = " ";
                              y_mid.Text = " ";
                              y2_coordinput.Text = " ";
                              gobutton.Text = "Go!!!";
                              output_coord.Text = "( , )";
                              peace_of_mind_counter = 0;

                              ball_clock.Enabled = false;
                              delta_x1 = 0;
                              delta_x2 = 0;
                              delta_y1 = 0;
                              delta_y2 = 0;
                              x_coord = 0;
                              y_coord = 0;

                              line_drawer.Invalidate();
                              line_drawer.Refresh();
                              break;
                  }
            }

      protected void ballHelper(Object sender, EventArgs events)
      {
            
      if(peace_of_mind_counter < BALL_MOVEMENTS)
      {           
                  x_coord += delta_x1;
                  y_coord += delta_y1;
      }
      else if(peace_of_mind_counter < (2 * BALL_MOVEMENTS))
      {
                  x_coord += delta_x2;
                  y_coord += delta_y2;
      }

            output_coord.Text = "(" + (Convert.ToInt32(x_coord) + 10) + "," + (Convert.ToInt32(y_coord) + 10) + ")";
            line_drawer.Refresh(); 
            peace_of_mind_counter++;
            }
      protected void shutdown(System.Object sender, EventArgs even)                   //<== Revised for version 2.2
      {//This function is called when the clock makes its first "tick", 
      //which occurs 3.5 seconds after the clock starts.
      Close();       //That means close the main user interface window.
      }//End of method shutdown

      

      */
      
      // Method to show a whole bunch of tiny funny red dots in the shape of an Exit Sign
      public class Graphicpanel: Panel
      {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Yellow);
      public Graphicpanel() 
            {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal

      //Draws the Arrow
      protected override void OnPaint(PaintEventArgs ee)
      {  
            Graphics graph = ee.Graphics;

            /*
            switch(runtime)
            {
            case State.Init: 

                  Console.WriteLine("Nothing Is Being Drawn");
                  
            break;

            default:
                  Console.WriteLine("A Line Is Being Drawn");

                  Pen pen = new Pen(System.Drawing.Color.Black);
                  pen.Width = 3.0F;                  
                  
                  graph.DrawLine(pen, Convert.ToInt32(x1_coordinput.Text), Convert.ToInt32(y1_coordinput.Text), Convert.ToInt32(x_mid.Text), Convert.ToInt32(y_mid.Text));
                  graph.DrawLine(pen, Convert.ToInt32(x_mid.Text), Convert.ToInt32(y_mid.Text), Convert.ToInt32(x2_coordinput.Text), Convert.ToInt32(y2_coordinput.Text));

                  graph.FillEllipse(paint_brush,Convert.ToInt32(x_coord), Convert.ToInt32(y_coord),25,25);
                  
            break;
            }
            */

            base.OnPaint(ee);

      }//End of OnPaint

      }//End of class Graphicpanel

}//End of clas RicochetBallInterface