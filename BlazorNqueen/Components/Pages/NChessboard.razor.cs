namespace BlazorNqueen.Components.Pages;

public partial class NChessboard
{
    [Parameter]
    public int BoardSize { get; set; }

    [Parameter]
    public int CanvasSize { get; set; }

    [Parameter]
    public bool ShowDebuggingTools { get; set; }

    // Update the board size
    public async Task SetBoardSize(int size)
    {
        logger.LogInformation("SetBoardSize");
        BoardSize = size;
        await Task.Delay(100);
        RenderLabels();
        StateHasChanged();
    }

    public void ClearBoard()
    {
        logger.LogInformation("ClearBoard");
        _chessJSmodule.InvokeVoidAsync("clearNBoard");
    }

    public void RenderLabels()
    {
        logger.LogInformation("RenderLabels");
        _chessJSmodule.InvokeVoidAsync("redrawLabels");
    }

    public void SetQueen(int col, int row)
    {
        logger.LogInformation($"SetQueen: {col}-{row}");
        _chessJSmodule.InvokeVoidAsync("setQueen", col, row);
        InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            logger.LogInformation("OnAfterRenderAsync");
            _chessJSmodule = await JS.InvokeAsync<IJSObjectReference>("import", "./js/chessInterop.js?v=1.0.0");
        }
    }

    private void SetQueenOnRandomSquare()
    {
        Random random = new();
        var row = random.Next(0, BoardSize);
        var col = random.Next(0, BoardSize);
        SetQueen(row, col);
    }

    private async void ChangeBoardSize()
    {
        BoardSize = BoardSize == 8 ? 16 : 8;
        await SetBoardSize(BoardSize);
    }

    private string _whiteSquareColor = "#f0d9b5"; // Default color for white squares
    private string _blackSquareColor = "#b58863"; // Default color for black squares
    private IJSObjectReference _chessJSmodule; //chessinterop.js
}
