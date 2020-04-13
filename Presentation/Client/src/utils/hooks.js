import { useEffect, useState } from "react";
import axios from "axios";

const safeInvoke = (func, ...args) => {
    if (typeof func === "function") {
        return func(...args);
    }
};

export const useFetch = ({ url, onComplete, onError }) => {
    const [loading, setLoading] = useState(false);
    const doFetch = async () => {
        try {
            setLoading(true);
            const { data } = await axios.get(url);
            setLoading(false);
            safeInvoke(onComplete, data);
        } catch (error) {
            console.log(error);
            safeInvoke(onError, error);
            setLoading(false);
        }
    };

    return { loading, doFetch };
};

export const usePost = (url, options) => {
    const [response, setResponse] = useState(null);
    const [error, setError] = useState(null);
    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await axios.post(url, options);
                const json = await res.json();
                setResponse(json);
            } catch (error) {
                setError(error);
            }
        };
        fetchData();
    }, []);
    return { response, error };
};
