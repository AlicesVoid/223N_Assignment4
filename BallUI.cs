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
      private Panel controls          = new Panel();
      private Graphicpanel ball_mover = new Graphicpanel();
      private Button init_button  = new Button();
      private Button start_button = new Button();
      private Button quit_button  = new Button();
      private TextBox speed_input = new TextBox();
      private TextBox dir_input   = new TextBox();
      private Textbox x_output    = new TextBox();
      private Textbox y_output    = new TextBox();

      //UI STYLE VARIABLES
      private Size program_size   = new Size(1000, 1200);
      private Size button_size    = new Size(150, 100);
      private Font control_font      = new Font("Comic Sans MS", 18, FontStyle.Regular);
      private Font title_font        = new Font("Impact", 25, Fontstyle.Bold);
      private TextAlign normal_align = new ContentAlignment.MiddleCenter();
      private BackColor header_color           = new Color.LightCoral();
      private BackColor ball_color             = new Color.Red();
      private BackColor ball_contrast          = new Color.LightCyan();
      private BackColor control_contrast       = new Color.LightGoldenrodYellow();
      private BackColor control_text_color     = new Color.LightSeaGreen();
      private BackColor control_button_color   = new Color.LightPink();

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
            controls.Size     = new Size(1000, 300);
            coord_title.Size  = new Size(350, 60);
            speed_title.Size  = new Size(350, 60);
            dir_title.Size    = new Size(350, 60);
            x_title.Size      = new Size(150, 90);
            y_title.Size      = new Size(150, 90);
            init_button.Size  = button_size;
            start_button.Size = button_size;
            quit_button.Size  = button_size;
            speed_input.Size  = button_size;
            dir_input.Size    = button_size;
            x_output.Size     = new Size(120, 90);
            y_output.Size     = new Size(120, 90);
            
            //INIT. COLORS
            header.BackColor       = header_color;
            ball_mover.BackColor   = ball_contrast;
            controls.BackColor     = control_contrast;
            coord_title.BackColor  = control_text_color;
            speed_title.BackColor  = control_text_color;
            dir_title.BackColor    = control_text_color;
            x_title.BackColor      = control_text_color;
            y_title.BackColor      = control_text_color;
            speed_input.BackColor  = control_button_color;
            dir_input.BackColor    = control_button_color;
            x_output.BackColor     = control_button_color;
            y_output.BackColor     = control_button_color; 
            init_button.BackColor  = control_button_color;
            start_button.BackColor = control_button_color;
            quit_button.BackColor  = control_button_color;

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
            title.TextAlign        = normal_align;
            coord_title.TextAlign  = normal_align;
            speed_title.TextAlign  = normal_align;
            dir_title.TextAlign    = normal_align;
            x_title.TextAlign      = normal_align;
            y_title.TextAlign      = normal_align;
            init_button.TextAlign  = normal_align;
            start_button.TextAlign = normal_align;
            quit_button.TextAlign  = normal_align;
            speed_input.TextAlign  = normal_align;
            dir_input.TextAlign    = normal_align;
            x_output.TextAlign     = normal_align;
            y_output.TextAlign     = normal_align;

            //INIT. LOCATIONS
            header.Location     = new Point(0, 0);
            ball_mover.Location = new Point(0, 200);
            controls.Location   = new Point(0, 900);

            title.Location = new Point(125,37);
            /*
            headerpanel.Location = new Point(0,0);
            title.Location = new Point(125,69);

                  start_coord.Location = new Point (55, 2);
                  mid_coord.Location = new Point (210, 18);
                  fin_coord.Location = new Point (400, 2);
                  x1_coordinput.Location = new Point(45, 72);
                  y1_coordinput.Location = new Point(110, 72);
                  x_mid.Location = new Point(215, 88);
                  y_mid.Location = new Point(280, 88);
                  x2_coordinput.Location = new Point(395, 72);
                  y2_coordinput.Location = new Point(460, 72);
                  local_coord.Location = new Point(550, 30);
                  output_coord.Location = new Point(570, 70);

            gobutton.Location = new Point(780,50);
            initialbutton.Location = new Point (900, 50);
            quitbutton.Location = new Point(1020,50);

            headerpanel.Location = new Point(0,0);
            line_drawer.Location = new Point(0,200);
            controlpanel.Location = new Point(0,760);
            */

            //Add controls to the form
            Controls.Add(headerpanel);
            headerpanel.Controls.Add(title);
            Controls.Add(line_drawer);
            Controls.Add(controlpanel);
            controlpanel.Controls.Add(gobutton);
            controlpanel.Controls.Add(initialbutton);
            controlpanel.Controls.Add(quitbutton);
            controlpanel.Controls.Add(x1_coordinput);
            controlpanel.Controls.Add(y1_coordinput);
            controlpanel.Controls.Add(x2_coordinput);
            controlpanel.Controls.Add(y2_coordinput);
            controlpanel.Controls.Add(x_mid);
            controlpanel.Controls.Add(y_mid);
            controlpanel.Controls.Add(start_coord);
            controlpanel.Controls.Add(mid_coord);
            controlpanel.Controls.Add(fin_coord);
            controlpanel.Controls.Add(local_coord);
            controlpanel.Controls.Add(output_coord);

            //Register the event handler.  In this case each button has an event handler, but no other 
            //controls have event handlers.
            gobutton.Click += new EventHandler(drawline);
            initialbutton.Click += new EventHandler(resetrun);
            quitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

            //Configure the clock that controls the shutdown      //<== New in version 2.2
            exit_clock.Enabled = false;     //Clock is turned off at start program execution.
            exit_clock.Interval = 7500;     //7500ms = 7.5seconds.  Clock will tick at intervals of 7.5 seconds
            exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

            ball_clock.Enabled = false;     //Clock is turned off at start program execution.
            ball_clock.Interval = ball_interval;     // 3.0 Hz
            ball_clock.Elapsed += new ElapsedEventHandler(ballHelper);   //Attach a method to the clock.


            //Open this user interface window in the center of the display.
            CenterToScreen();

      }//End of constructor RicochetBallInterface
      


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



      // Method to show a whole bunch of tiny funny red dots in the shape of an Exit Sign
      public class Graphicpanel: Panel
      {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Yellow);
      public Graphicpanel() 
            {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal

      //Draws the Arrow
      protected override void OnPaint(PaintEventArgs ee)
      {  
            Graphics graph = ee.Graphics;

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

            base.OnPaint(ee);

      }//End of OnPaint

      }//End of class Graphicpanel

}//End of clas RicochetBallInterface