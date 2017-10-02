import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface FeedsState {
    optionsIsLoading: boolean;
    feedsIsLoading: boolean;
    feedOptions: FeedOptions[];
    feeds: Feed[];
}

export interface FeedOptions {
    id: number;
    name: string;
}

interface RequestFeedOptionsAction {
    type: 'REQUEST_FEED_OPTIONS';
}

interface ReceiveFeedOptionsAction {
    type: 'RECEIVE_FEED_OPTIONS';
    feedOptions: FeedOptions[];
}

export interface Feed {
    title: string;
    description: string;
}

interface RequestFeedsAction {
    type: 'REQUEST_FEEDS';
    ids: number[]
}

interface ReceiveFeedsAction {
    type: 'RECEIVE_FEEDS';
    feeds: Feed[];
}

type KnownAction = RequestFeedOptionsAction | ReceiveFeedOptionsAction | RequestFeedsAction | ReceiveFeedsAction;

export const actionCreators = {
    requestFeedOptions: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let fetchTask = fetch(`api/feeds/options`)
            .then(response => response.json() as Promise<FeedOptions[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_FEED_OPTIONS', feedOptions: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_FEED_OPTIONS' });
    },
    requestFeeds: (ids: number[]): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let fetchTask = fetch(`api/feeds?ids=${ids.join()}`)
            .then(response => response.json() as Promise<Feed[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_FEEDS', feeds: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_FEEDS', ids: ids });
    }
};

const unloadedState: FeedsState = { feedOptions: [], optionsIsLoading: false, feeds: [], feedsIsLoading: false };

export const reducer: Reducer<FeedsState> = (state: FeedsState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_FEED_OPTIONS':
            return {
                feeds: state.feeds,
                feedsIsLoading: state.feedsIsLoading,
                feedOptions: [],
                optionsIsLoading: true
            };
        case 'RECEIVE_FEED_OPTIONS':
            return {
                feeds: state.feeds,
                feedsIsLoading: state.feedsIsLoading,
                feedOptions: action.feedOptions,
                optionsIsLoading: false
            };
        case 'REQUEST_FEEDS':
            return {
                feeds: [],
                feedsIsLoading: true,
                feedOptions: state.feedOptions,
                optionsIsLoading: state.optionsIsLoading
            };
        case 'RECEIVE_FEEDS':
            return {
                feeds: action.feeds,
                feedsIsLoading: false,
                feedOptions: state.feedOptions,
                optionsIsLoading: state.optionsIsLoading
            };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
