Public Class SnakeAlgorithm
    Private Class Node
        Inherits Position
        Implements IComparable(Of Node), IEquatable(Of Node)

        ' GCost = how many steps we have taken, HCost = how many steps away from the goal assuming no obstacles
        Public GCost As Integer, HCost As Integer, Direction As Direction, ParentNode As Node, Priority As Integer, SearchDepth As Integer

        Public ReadOnly Property FCost As Integer
            Get
                Return GCost + HCost
            End Get
        End Property

        Public Sub New(Position As Position, Direction As Direction, ParentNode As Node)
            Me.New(Position.X, Position.Y, Direction, ParentNode)
        End Sub

        Public Sub New(X As Integer, Y As Integer, Direction As Direction, ParentNode As Node)
            Me.ParentNode = ParentNode
            Me.Direction = Direction
            Me.X = X
            Me.Y = Y
            GCost = Integer.MaxValue
            HCost = 0
            SearchDepth = 0
            If ParentNode IsNot Nothing Then
                SearchDepth = ParentNode.SearchDepth + 1
            End If
        End Sub

        Public Function CompareTo(other As Node) As Integer Implements IComparable(Of Node).CompareTo
            If other Is Nothing Then
                Return 1
            End If

            Dim PriorityComparison As Integer = Priority.CompareTo(other.Priority)
            If PriorityComparison <> 0 Then Return -PriorityComparison

            Return FCost.CompareTo(other.FCost)
        End Function

        Public Function Equals(other As Node) As Boolean Implements IEquatable(Of Node).Equals
            If other Is Nothing Then
                Return False
            End If

            Return X = other.X And Y = other.Y And GCost = other.GCost And HCost = other.HCost
        End Function

        Public Function PositionEquals(other As Position) As Boolean
            If other Is Nothing Then
                Return False
            End If

            Return X = other.X And Y = other.Y
        End Function
    End Class

    Private Class NodeWithCells
        Inherits Node
        Public Cells As List(Of Position)

        Public Sub New(Node As Node, Cells As List(Of Position))
            MyBase.New(Node.X, Node.Y, Node.Direction, Node.ParentNode)
            Priority = Node.Priority
            GCost = Node.GCost
            HCost = Node.HCost
            Me.Cells = Cells
        End Sub
    End Class

    Public Class PathFindingResult
        Public Path As List(Of Position)
        Public Direction As Direction

        Public ReadOnly Property FoundPath As Boolean
            Get
                Return Path IsNot Nothing And Direction <> Direction.None
            End Get
        End Property

        Public Sub New(Path As List(Of Position), Direction As Direction)
            Me.Path = Path
            Me.Direction = Direction
        End Sub
    End Class

    Private Game As SnakeGame

    Public Sub New(ByRef game As SnakeGame)
        Me.Game = game
    End Sub

    Public Function PathFindSnake() As PathFindingResult
        ' TODO
        Return PathFind(Game.SnakePosition, Game.ApplePosition, True)
    End Function

    Private Function CheckAvailable(InitialPosition As Position, Grid(,) As Boolean) As Boolean
        ' check if the position is available from current location

        Dim AvailableGrid(Game.GameSize.X, Game.GameSize.Y) As Boolean

        ' clone from original grid
        For X = 0 To Game.GameSize.X - 1
            For Y = 0 To Game.GameSize.Y - 1
                AvailableGrid(X, Y) = Grid(X, Y)
            Next
        Next

        ' create queue with current node as the first one to loop through
        Dim FloodFillQueue As New List(Of Position) From {InitialPosition}

        ' set initial cell to true
        AvailableGrid(InitialPosition.X, InitialPosition.Y) = True

        Dim FirstCell As Boolean = False

        While FloodFillQueue.Count > 0
            Dim LoopPosition As Position = FloodFillQueue(0)
            FloodFillQueue.RemoveAt(0)

            ' get neighbor cells
            For Each Direction As Direction In [Enum].GetValues(GetType(Direction))
                If Direction = Direction.None Then Continue For
                Dim NextPosition As Position = LoopPosition.Add(Direction)
                If Game.OutOfBounds(NextPosition) Then Continue For

                ' if cell is false
                If Not AvailableGrid(NextPosition.X, NextPosition.Y) Then
                    ' add that cell to the queue
                    FloodFillQueue.Add(NextPosition)

                    ' set cell to true
                    AvailableGrid(NextPosition.X, NextPosition.Y) = True

                    ' only check one side of the new cell that might have cut off the rest of the grid
                    If Not FirstCell Then
                        FirstCell = True
                        Exit For
                    End If
                End If
            Next
        End While

        For X = 0 To Game.GameSize.X - 1
            For Y = 0 To Game.GameSize.Y - 1
                ' if there are any false values left over in the grid, not all tiles are available
                If Not AvailableGrid(X, Y) Then
                    PrintGrid(Grid, AvailableGrid, InitialPosition)
                    Return False
                End If
            Next
        Next

        Return True
    End Function

    Private Sub PrintGrid(Grid(,) As Boolean, Grid2(,) As Boolean, Position As Position)
        Console.WriteLine(New String("=", Game.GameSize.X))
        Console.WriteLine(Position.X & " " & Position.Y)

        For Y = 0 To Game.GameSize.Y - 1
            For X = 0 To Game.GameSize.X - 1
                Console.Write(If(Grid(X, Y), "#", If(Grid2 IsNot Nothing AndAlso Grid2(X, Y), "-", ".")))
                Console.Write(If(X = Position.X And Y = Position.Y, "+", " "))
            Next
            Console.WriteLine()
        Next

        Console.WriteLine(New String("=", Game.GameSize.X))
        Console.WriteLine()
    End Sub

    Private Function PathFind(StartPosition_ As Position, GoalPosition As Position, IgnoreCellAvailability As Boolean) As PathFindingResult
        Dim Grid(Game.GameSize.X, Game.GameSize.Y) As Boolean

        Dim StartPosition As New Node(StartPosition_, Direction.None, Nothing)

        Dim OpenList As New List(Of NodeWithCells)
        Dim ClosedList As New List(Of Node)

        ' calculate costs
        StartPosition.GCost = 0
        StartPosition.HCost = CalculateHeuristic(StartPosition, GoalPosition)

        OpenList.Add(New NodeWithCells(StartPosition, Game.SnakeCells))

        While OpenList.Count > 0
            OpenList.Sort()

            Dim CurrentNode As NodeWithCells = OpenList(0)
            OpenList.RemoveAt(0)

            ' return path if we've reached the goal
            If GoalPosition IsNot Nothing AndAlso GoalPosition.Equals(CurrentNode) Then
                Return GetPathResult(CurrentNode)
            End If

            ' add to closed set
            ClosedList.Add(CurrentNode)

            ' loop all directions
            For Each Direction As Direction In [Enum].GetValues(GetType(Direction))
                If Direction = Direction.None Then GoTo nextDirection

                Dim NewPosition As Position = CurrentNode.Add(Direction)

                ' don't go out of bounds
                If Game.OutOfBounds(NewPosition) Then GoTo nextDirection

                ' clone the snake cells of the last node
                Dim SnakeCells As List(Of Position) = CloneCells(CurrentNode.Cells)

                ' don't go in to snake
                For Each Cell In SnakeCells
                    If Cell.Equals(NewPosition) Then GoTo nextDirection
                Next

                ' simulate the snake moving
                SnakeCells.Add(NewPosition)
                While SnakeCells.Count > Game.SnakeLength
                    SnakeCells.RemoveAt(0)
                End While

                ' create grid for snake cells
                For X = 0 To Game.GameSize.X - 1
                    For Y = 0 To Game.GameSize.Y - 1
                        Grid(X, Y) = False
                    Next
                Next
                SnakeCells.ForEach(Sub(pos) Grid(pos.X, pos.Y) = True)

                Dim NewNode As Node = New Node(NewPosition, Direction, CurrentNode)

                ' prefer nodes that are next to another snake cell
                Dim AdjacentCount As Integer = GetAdjacentSnakeCells(NewNode, Direction, Grid)
                NewNode.Priority = AdjacentCount

                NewNode.GCost = CurrentNode.GCost + 1

                If ClosedList.Any(Function(node) NewNode.PositionEquals(node)) Then GoTo nextDirection
                If IsDuplicate(OpenList, NewNode) Then GoTo nextDirection

                If Not IgnoreCellAvailability AndAlso Not CheckAvailable(CurrentNode, Grid) Then GoTo nextDirection

                NewNode.HCost = CalculateHeuristic(NewNode, GoalPosition)

                OpenList.Add(New NodeWithCells(NewNode, SnakeCells))

nextDirection:
            Next
        End While

        If Not IgnoreCellAvailability Then
            Return PathFind(StartPosition_, GoalPosition, True)
        End If

        ClosedList.Sort()
        ClosedList.Sort(Comparer(Of Node).Create(Function(a As Node, b As Node) a.SearchDepth.CompareTo(b.SearchDepth)))
        If ClosedList.Count > 0 Then
            Dim Node As Node = ClosedList.First()
            While Node.ParentNode IsNot Nothing AndAlso Node.ParentNode.ParentNode IsNot Nothing
                Node = Node.ParentNode
            End While
            Return New PathFindingResult(Nothing, Node.Direction)
        Else
            Return New PathFindingResult(Nothing, Direction.None)
        End If
    End Function

    Private Function CloneCells(Cells As List(Of Position)) As List(Of Position)
        Dim NewCells As New List(Of Position)
        Cells.ForEach(Sub(Cell) NewCells.Add(New Position(Cell)))
        Return NewCells
    End Function

    Private Directions8 As New List(Of Position) From {
        New Position(-1, -1),
        New Position(0, -1),
        New Position(1, -1),
        New Position(-1, 0),
        New Position(1, 0),
        New Position(-1, 1),
        New Position(0, 1),
        New Position(1, 1)
    }

    Private Function GetAdjacentSnakeCells(Node As Node, ExcludeDirection As Direction, Grid(,) As Boolean) As Integer
        Dim Count As Integer = 0
        ' loop all 8 directions
        For Each Direction As Position In Directions8
            If GetOppositeDirection(ExcludeDirection).equals(Direction) Then Continue For
            Dim AdjacentPosition As Position = Node.Add(Direction)
            If Game.OutOfBounds(AdjacentPosition) Then Continue For

            ' add 2 priority if the neighbour is not diagonal, otherwise 1
            If Grid(AdjacentPosition.X, AdjacentPosition.Y) Then Count += If(Direction.X = 0 Or Direction.Y = 0, 2, 1)
        Next
        Return Count
    End Function

    Private Function IsDuplicate(Collection As List(Of NodeWithCells), NewNode As Node) As Boolean
        For Each SetNode As NodeWithCells In Collection
            ' if the node in the collection is the same position as the new node
            If NewNode.PositionEquals(SetNode) Then
                ' if the new node does NOT have the minimum gcost value for that position in the collection, return true
                If SetNode.GCost <= NewNode.GCost Then
                    Return True
                End If
            End If
        Next

        ' if we haven't already explored this position, or we have the shortest path to the position compared to the other paths to the position, return false
        Return False
    End Function

    Private Function GetPathResult(CurrentNode As Node) As PathFindingResult
        Dim List As New List(Of Position)
        Dim PreviousNode As Node = CurrentNode

        While CurrentNode.ParentNode IsNot Nothing
            ' add position to path
            List.Add(CurrentNode)
            PreviousNode = CurrentNode
            ' loop to next parent
            CurrentNode = CurrentNode.ParentNode
        End While

        ' add first position
        List.Add(CurrentNode)

        ' reverse list so first is the start not goal
        List.Reverse()

        Return New PathFindingResult(List, PreviousNode.Direction)
    End Function

    Private Function CalculateHeuristic(StartPosition As Position, GoalPosition As Position) As Integer
        If GoalPosition Is Nothing Then
            Return 0
        End If
        Return Math.Abs(StartPosition.X - GoalPosition.X) + Math.Abs(StartPosition.Y - GoalPosition.Y)
    End Function
End Class
