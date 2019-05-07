class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">Hello, world! I am a CommentBox 1.</div>
        );
    }
}


class Background extends React.Component {
    render() {
        return (
            <div className="commentBox">Hello, world! I am a CommentBox 234356.</div>
        );
    }
}


class MessageBox extends React.Component {
    render() {
        return (
            <div>{this.props.message}</div>
        );
    }
}





// Render an instance of MessageComponent into document.body
//ReactDOM.render(<MessageBox message="Hello!" />, document.getElementById('content'));
ReactDOM.render(
    <div>
        <MessageBox message='Hello!' />
        <Background />
    </div>, document.getElementById('content'));

