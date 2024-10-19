using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.InteropServices;

public class Matriz{
    private int[,] matriz;
    private int[,] visible;// invisible = 0 | visble= 1 | Flag = 2
    private int rows;
    private int cols;
    private int mines;
    private int state = 0;
    private int EmptyCells;
    private int OpenCells = 0;
    public Matriz(int rows, int cols, int mines){
        this.rows = rows;
        this.cols = cols;
        this.mines  = mines;
        this.EmptyCells = (rows * cols) - mines;
        this.OpenCells = 0;
        matriz = new int[rows, cols];
        visible = new int[rows, cols];
        PutMines();
        PutNumbers();
    }   

    public int getState(){
        return state;
    }
    public void CLearMatriz(){
        for (int i = 0; i < matriz.GetLength(0); i++){
            for (int j = 0; j < matriz.GetLength(1); j++){
                matriz[i,j] = 0;
                visible[i,j] = 0;
            }
        }
    }
    public override string ToString() {
        string text = "";
    
        for (int i = 0; i < matriz.GetLength(0); i++){
            for (int j = 0; j < matriz.GetLength(1); j++){
                text += " | ";
                switch(visible[i,j]){
                    case 0: 
                        text += "-"; 
                        break;
                    case 1:
                        if(matriz[i, j] == -1){
                            text += "M";
                        } else if(matriz[i, j] == 0){
                            text += " ";
                        } else {
                            text += matriz[i, j];
                        }
                        break;
                    case 2:
                         text += "F"; 
                        break;
                }
            }
            text += " |\n";
        }
        return text;
    }
    public void PutMines(){
        CLearMatriz();
        Coord coord  =new Coord(this.rows,this.cols);
        for(int i = 0; i <this.mines; i++){  
            if(matriz[coord.X,coord.Y] != -1){
                matriz[coord.X,coord.Y] = -1;
            } else{
                i--;
            }
            coord.Next();
        }
    }
    public void PutNumbers(){
        for (int i = 0; i < matriz.GetLength(0); i++){
            for (int j = 0; j < matriz.GetLength(1); j++){
                if (matriz[i,j] == -1){
                    for (int a = i-1; a <= i+1; a++){
                        for (int b = j-1; b <= j+1; b++){
                            if(isInMatriz(a,b) && matriz[a,b] != -1){
                                matriz[a,b]++;
                            }
                        }
                    }
                }
            }
        }
    }

    public bool isInMatriz(int row, int col){
            if(row >= 0 && row < matriz.GetLength(0) && col >= 0 && col < matriz.GetLength(1)){
                return true;
            }
            return false;
    }
    public int GetLength(int i){
        return matriz.GetLength(i);
    }

    public int doClick(int x, int y){
        if(isInMatriz(x,y)){
            if(visible[x,y] == 0){
                visible[x,y] = 1;
                if(matriz[x,y] == 0){
                    this.OpenCells++;
                    for (int a = x-1; a <= x+1; a++){
                        for (int b = y-1; b <= y+1; b++){
                            if(isInMatriz(a,b)){
                                doClick(a,b);
                            }
                        }
                    }
                } else if(matriz[x,y] == -1){
                    this.state = 2;
                } else {
                    this.OpenCells++;
                }
            }
        }
        if(OpenCells >= EmptyCells){
            this.state = 1;
        }
        return this.state;
    }
    public void putFlag(int x, int y){
        if(isInMatriz(x,y)){
            if(this.visible[x,y] == 0){
                this.visible[x,y] = 2;
            }else if(this.visible[x,y] == 2){
                this.visible[x,y] = 0;
            }
        }
    }
}
public class Coord{
    public int X{get;set;}
    public int Y{get;set;}
    private int minX;
    private int maxX;
    private int minY;
    private int maxY;
    public Coord(){
        minX = 0;
        maxX = 100;
        minY = 0;
        maxY = 100;
        this.Next();
    }
    public Coord(int maxX, int maxY){
        this.minX = 0;
        this.maxX = maxX;
        this.minY = 0;
        this.maxY = maxY;
        this.Next();
    }
     public Coord(int minX, int maxX, int minY, int maxY){
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        this.Next();
    }
    public void setNewConfig(int minX, int maxX, int minY, int maxY){
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }
        public void setNewConfig(int maxX, int maxY){
        this.minX = 0;
        this.maxX = maxX;
        this.minY = 0;
        this.maxY = maxY;
    }

    public Coord Next(){
        Random rnd = new Random();
        this.X = rnd.Next(this.minX, this.maxX);
        this.Y = rnd.Next(this.minY, this.maxY);
        return this;
    }

    public override string ToString() {
        return $"({X}, {Y})";
    }
}
public class Minesweepper{

    static void Main(string[] args){
        Examen ex = new Examen();
        ex.ej();
        //Game game= new Game();
        //game.init();
    }
}

public class Examen(){

    public void ej(){
    //////
|   do{
        //codigo
    }while(true);
    //////
    
    }
}


public class Game(){

    int height = 0;
    int weight = 0;
    int mines = 0;
    Matriz matriz;
    int dificultad = 0;
    public void init(){
        Console.Clear();
        WriteLine("Bienvenidos a mi Buscaminas... ojala no encuentres un traba.");
        WriteLine("Antes que nada...");
        WriteLine("En que dificultad te gustaria jugar?");
        WriteLine("1.- Facil");
        WriteLine("2.- Intermedio");
        WriteLine("3.- Dificil");
        switch(ReadInt()){
            case 1:
                dificultad = 0;
                WriteLine("BUE, QUERES QUE TE LO RESUELVA YO, TAMBIEN?");
                height = 10;
                weight = 10;
                mines = 10;
                break;
            case 2:
                 dificultad = 1;
                WriteLine("EL NI TIRA NI AFLOJA LE DECIAN.");
                height = 15;
                weight = 15;
                mines = 30;
                break;
            case 3:
                dificultad = 3;
                WriteLine("BUEEE SE CREE EL PIJALARGA, AHI TE VA LA DIFICIL.");
                height = 20;
                weight = 20;
                mines = 50;
                break;
            default:
                dificultad = 4;
                WriteLine("SOS INBECIL, DIJE 1, 2 o 3... DONDE CARAJOS VES ESA OPCION QUE PUSISTE.");
                WriteLine("AHORA POR DOLOBU TE VAS CON LA MAS DIFICIL.");
                height = 26;
                weight = 26;
                mines = 300;
                break;
        } 

        Console.WriteLine("Presiona tecla cualquiera...");
        Console.ReadKey();
        this.matriz = createMineSweeper();
        int state = 0; //0.- Sigue | 1.- Ganaste | 2.- Perdiste
        do{
            int row = 0;
            int col = 0;
            int flag = 0;
            Console.Clear();
            PrintMinesweepper();
            Console.Write("Letra de Columna: ");
            col = (int)ReadChar() - 65;
            Console.Write("Numero de Fila: ");
            row = ReadInt() - 1;
            Console.Write("1.-Destapar | 2.-Bandera | 3.-Cancelar: ");
            flag = ReadInt();
            switch (flag){
                case 1:   
                    this.matriz.doClick(row,col);
                    break;
                case 2:
                    this.matriz.putFlag(row,col);
                    break;
                default:
                    break;
            }
            state = this.matriz.getState();
        }while(state <= 0);

        Console.Clear();
        PrintMinesweepper();
        if(state == 1){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("GANASTE!!!");
            Console.ResetColor();
            Console.WriteLine("Bueno, Supongo que al final no eras un bueno para nada..Solo sabes resolver El buscaminas");
            switch(dificultad){
                case 1:
                    Console.WriteLine("Aunque, bueno estaba pensado para nenes de entre 2 y 3 años");
                    break;
                case 2:
                    Console.WriteLine("Aunque, bueno tambien un simio casi entrenado, pudo hacerlo");
                    break;
                case 3:
                    Console.WriteLine("Felicidades el nivel mas dificil..O no?");
                    break;
                case 4:
                    Console.WriteLine("Supongo que eres un Idiota pero No tanto... Ten una porcion de Tarta");
                    break;
            }
        } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PERDEDOOORR!!!!");
            Console.ResetColor();
            Console.WriteLine("Felicidades, Cumplistes con las espectativas que pense que cumplirias.. Osea Ninguna.");
            switch(dificultad){
                case 1:
                    Console.WriteLine("Un recien nacido pudiera haberlo completado.");
                    break;
                case 2:
                    Console.WriteLine("No te sientas mal, era demasiado para ti.. Intenta hacer uno mas facil, asi como de tu nivel.");
                    break;
                case 3:
                    Console.WriteLine("Y creias que esta era tu dificultad?.");
                    break;
                case 4:
                    Console.WriteLine("la proxima vez no me haras enojar.");
                    break;
            }
        }
        Console.WriteLine("Presiona Cerrar el programa, por favor, ya no te quiero ver aqui...");
        Console.ReadKey();
    }

    public void PrintMinesweepper(){
        for (int i = 0; i < matriz.GetLength(0); i++){
            char letter = (char)(65+i);
            Console.Write( " | " + letter);
        }
        Console.WriteLine( " | ");
        for (int i = 0; i < matriz.GetLength(0); i++){
            Console.Write( " - -" );
        }
        Console.WriteLine(" -");
        string text = matriz.ToString();
        int row = 1;
        foreach(char letter in text){
            if(letter == 'M'){
                Console.ForegroundColor = ConsoleColor.Red;
            } else if(letter == '1'){
                Console.ForegroundColor = ConsoleColor.Yellow;
            } else if(letter == '2'){
                Console.ForegroundColor = ConsoleColor.Blue;
            } else if(letter == '3'){
                Console.ForegroundColor = ConsoleColor.Green;
            } else if(letter == '4'){
                Console.ForegroundColor = ConsoleColor.Cyan;
            } else if(letter == '5'){
                Console.ForegroundColor = ConsoleColor.Magenta;
            } else if(letter == '6'){
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            } else if(letter == '7'){
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            } else if(letter == '8'){
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            } else if(letter == '9'){
                Console.ForegroundColor = ConsoleColor.DarkRed;
            } else if(letter == '-'){
                Console.ForegroundColor = ConsoleColor.Gray;
            } else if (letter == '\n' || letter == '\r'){
                Console.Write(" "+ row);
                row++;
            } else {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(letter);
            Console.ResetColor();
        }
    }
    public Matriz createMineSweeper(){
        return new Matriz(height, weight, mines);
    }

    public int ReadInt(){
        int number;
        bool isValid;
        do{
            string? entry = Console.ReadLine();

            isValid = int.TryParse(entry, out number);
            if(!isValid){
                WriteLine("La Entrada NO es valida.. Sos Mogolico? DIJE UN NUMERO (obviamente uno entero).");
            }
        }while(!isValid);
        return number;
    }

public char ReadChar(){
    char character;
    bool isValid;
    do{
        string? entry = Console.ReadLine();

        isValid = !string.IsNullOrEmpty(entry) && entry.Length == 1;
        if (isValid){
            character = char.ToUpper(entry[0]);
            isValid = character >= 'A' && character <= 'Z';
        }else{
            character = ' ';
        }
        if (!isValid){
            Console.WriteLine("La entrada NO es válida.");
        }
    } while (!isValid);
    return character;
}
    public void WriteLine(string text, int velocity = 4){
        foreach(char letter in text){
            Console.Write(letter);
            switch(velocity){
                case 1:
                    Thread.Sleep(200);
                    break;
                case 2:
                    Thread.Sleep(100);
                    break;
                case 3:
                    Thread.Sleep(50);
                    break;
                default:
                    Thread.Sleep(1);
                    break;
            }
            
        }
        Console.WriteLine();
    }
}

/*
para correr en consola

C:\Users\Deffiner\Desktop\BLA

dotnet build

dotnet run --project C:\Users\Deffiner\Desktop\BLA\Minesweeper

*/