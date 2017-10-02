import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as Feeds from '../store/Feeds';

type HomeProps =
    Feeds.FeedsState
    & typeof Feeds.actionCreators
    & RouteComponentProps<{}>;

class Home extends React.Component<HomeProps, {}> {
    private options : any[] = [];

    componentWillMount() {
        this.props.requestFeedOptions();
    }

    public render() {
        return <div>
            <h1>Feed Reader</h1>
            {this.props.optionsIsLoading ? <span>Loading...</span> : this.renderOptions()}
            {this.props.feedsIsLoading ? <span>Loading Feeds...</span> : this.renderFeeds()}
        </div>;
    }

    private renderOptions() {
        return <div>
            {this.props.feedOptions.map(feedOption =>
                <div>
                    <input type="checkbox" value={feedOption.id} key={feedOption.id} ref={(checkbox) => this.options[feedOption.id] = checkbox} />
                    {feedOption.name}
                </div>
            )}
            <input type="button" onClick={() => this.getFeeds()} value="Get Feeds" />
        </div>;
    }

    private renderFeeds() {
        return <ul>
            {this.props.feeds.map(feed =>
                <li>
                    <div>
                        {feed.title}
                    </div>
                    <div>
                        {feed.description}
                    </div>
                </li>
            )}
        </ul>;
    }

    private getFeeds() {
        let ids : number[] = [];

        for (let i = 0; i < this.options.length; i++) {
            if (this.options[i] && this.options[i].checked) {
                ids.push(parseInt(this.options[i].value));
            }
        }

        this.props.requestFeeds(ids);
    }
}

export default connect(
    (state: ApplicationState) => state.feeds, 
    Feeds.actionCreators
)(Home) as typeof Home;
