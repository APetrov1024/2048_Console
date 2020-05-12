using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048_Model;
using Game2048_View;


namespace Game2048_Controller
{
    public class Controller
    {
        private enum GameStates { ExitCommandRecieved, Win, Fail, Running }
        private Model model;
        private View view;
        private bool isNotExitCommandRecived = true;
        private int targetValue;
        private InputHandler inputHandler = new InputHandler();
        private GameStates GameState { get; set; }

        public Controller()
        {
            this.targetValue = 2048;
            this.model = new Model(4);
            this.view = new View(this.model);
        }

        public Controller(int hFieldSize, int vFieldSize, int targetValue)
        {
            this.targetValue = targetValue;
            this.model = new Model(hFieldSize, vFieldSize);
            this.view = new View(this.model);
        }

        public void StartGame()
        {
            this.GameState = GameStates.Running;
            gameLoop();
            if (this.isNotExitCommandRecived)
            {
                this.view.Display();
                if (this.GameState == GameStates.Win)
                    this.view.DisplayWinMessage();
                if (this.GameState == GameStates.Fail)
                    this.view.DisplayFailMessage();
                Actions userAction = Actions.None;
                while (userAction != Actions.Exit && userAction != Actions.Restart)
                {
                    userAction = this.inputHandler.Listen();
                }
                DoAction(userAction);
            }
        }

        public void RestartGame()
        {
            this.model.ClearField();
            StartGame();
        }

        public void gameLoop()
        {
            while (this.isNotExitCommandRecived && this.GameState == GameStates.Running)
            {
                this.view.Display();
                Actions userAction = this.inputHandler.Listen();
                DoAction(userAction);
                if (this.model.IsHaveValue(this.targetValue))
                    this.GameState = GameStates.Win;
                if (this.model.IsHasNotMoves())
                    this.GameState = GameStates.Fail;
            }
        }

        private void DoAction(Actions action)
        {
            switch (action)
            {
                case Actions.Down:
                    model.Action(Model.Directions.Down);
                    break;
                case Actions.Up:
                    model.Action(Model.Directions.Up);
                    break;
                case Actions.Left:
                    model.Action(Model.Directions.Left);
                    break;
                case Actions.Right:
                    model.Action(Model.Directions.Right);
                    break;
                case Actions.Exit:
                    this.isNotExitCommandRecived = false;
                    break;
                case Actions.Restart:
                    RestartGame();
                    break;
            }
        }



    }
}
