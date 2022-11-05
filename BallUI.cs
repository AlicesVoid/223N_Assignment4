/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 9/17/2022
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;




public class BallMotionInterface: Form
{private Label title = new Label();
 private Button gobutton = new Button();
 private Button resetbutton = new Button();
 private Button quitbutton = new Button();
 private Panel headerpanel = new Panel();
 private Graphicpanel line_drawer = new Graphicpanel();
 private Panel controlpanel = new Panel();
 private Size maximumexitsign = new Size(1200,960);
 private Size minimumexitsign = new Size(1200,960);

 private Label start_coord = new Label();
 private Label mid_coord = new Label();
 private Label fin_coord = new Label();
 private Label local_coord = new Label();
 private Label output_coord = new Label();
 private static TextBox x1_coordinput = new TextBox();
 private static TextBox y1_coordinput = new TextBox();
 private static TextBox x2_coordinput = new TextBox();
 private static TextBox y2_coordinput = new TextBox();
 private static TextBox x_mid = new TextBox();
 private static TextBox y_mid = new TextBox();

 private static double BALL_MOVEMENTS = 30;

 private static double delta_x1 = 0;
 private static double delta_y1 = 0;
 private static double delta_x2 = 0;
 private static double delta_y2 = 0;
 private static double x_coord = 0;
 private static double y_coord = 0;
 private static double peace_of_mind_counter = 0;

 private enum State {Init, Line, Pause};
 private static State runtime = State.Init;


 private enum Execution_state {Executing, Waiting_to_terminate};             //<== New in version 2.2
 private Execution_state current_state = Execution_state.Executing;
 private static System.Timers.Timer exit_clock = new System.Timers.Timer();  //<== New in version 2.2
 private static System.Timers.Timer ball_clock = new System.Timers.Timer();  //<== New in version 2.2

private const double clock_time = 3.0; //Hz
private const double one_second = 1000.0; //ms
private const double ball_interval = one_second / clock_time;


 public BallMotionInterface()  //Constructor begins here
   {//Set the size of the user interface box.
    MaximumSize = maximumexitsign;
    MinimumSize = minimumexitsign;
    //Initialize text strings
    Text = "Ball In Motion by Amelia Rotondo";
    title.Text = "Ball In Motion by Amelia Rotondo";
    gobutton.Text = "Go!!!";
    resetbutton.Text = "Reset";
    quitbutton.Text = "Quit...";
    start_coord.Text = "Start:";
    mid_coord.Text = "Midpoint:";
    fin_coord.Text = "Finish:";
    local_coord.Text = "Ball Location:";
    output_coord.Text = "( , )";
    
    //Set sizes
    Size = new Size(400,240);
    title.Size = new Size(800,44);
    gobutton.Size = new Size(100,100);
    resetbutton.Size = new Size(100,100);
    quitbutton.Size = new Size(100,100);
    headerpanel.Size = new Size(1200,200);
    line_drawer.Size = new Size(1200,560);
    controlpanel.Size = new Size(1200,200);
    x1_coordinput.Size = new Size(60, 200);
    y1_coordinput.Size = new Size(60, 200);
    x2_coordinput.Size = new Size(60, 200);
    y2_coordinput.Size = new Size(60, 200);
    x_mid.Size = new Size(60, 200);
    y_mid.Size = new Size(60, 200);
    start_coord.Size = new Size(100, 80);
    mid_coord.Size = new Size(170, 80);
    fin_coord.Size = new Size(120, 80);
    local_coord.Size = new Size(250, 40);
    output_coord.Size = new Size(160, 60);
    
    //Set colors
    headerpanel.BackColor = Color.LightPink;
    line_drawer.BackColor = Color.Aquamarine;
    controlpanel.BackColor = Color.LightYellow;
    gobutton.BackColor = Color.LightSalmon;
    resetbutton.BackColor = Color.LightSalmon;
    quitbutton.BackColor = Color.Cyan;
    output_coord.BackColor = Color.White;
    //quitbutton.BackColor = Color.FromArgb(0xA1,0xD4,0xAA);
    
    //Set fonts
    title.Font = new Font("Impact",33,FontStyle.Bold);
    gobutton.Font = new Font("Comic Sans MS",20,FontStyle.Bold);
    resetbutton.Font = new Font("Comic Sans MS",20,FontStyle.Bold);
    quitbutton.Font = new Font("Comic Sans MS",20,FontStyle.Italic);
    x1_coordinput.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    y1_coordinput.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    x2_coordinput.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    y2_coordinput.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    x_mid.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    y_mid.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);
    start_coord.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
    mid_coord.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
    fin_coord.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
    local_coord.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
    output_coord.Font = new Font("Comic Sans MS", 20, FontStyle.Regular);  

    //Set position of text within a label
    title.TextAlign = ContentAlignment.MiddleCenter;
    start_coord.TextAlign = ContentAlignment.MiddleLeft;
    mid_coord.TextAlign = ContentAlignment.MiddleLeft; 
    fin_coord.TextAlign = ContentAlignment.MiddleLeft;
    output_coord.TextAlign = ContentAlignment.MiddleCenter;

    //Set locations
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
    resetbutton.Location = new Point (900, 50);
    quitbutton.Location = new Point(1020,50);

    headerpanel.Location = new Point(0,0);
    line_drawer.Location = new Point(0,200);
    controlpanel.Location = new Point(0,760);

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(title);
    Controls.Add(line_drawer);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(gobutton);
    controlpanel.Controls.Add(resetbutton);
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
    resetbutton.Click += new EventHandler(resetrun);
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

   }//End of constructor BallMotionInterface
   


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


}//End of clas BallMotionInterface