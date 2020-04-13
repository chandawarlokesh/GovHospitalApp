import React, { useCallback, useEffect } from "react";
import { useForm } from "react-hook-form";
import axios from "axios";
import { useFetch } from "utils/hooks";

const Hospital = ({ hospital, doFetch, editable }) => {
    const { register, handleSubmit } = useForm({
        defaultValues: hospital,
    });

    const onSubmit = useCallback(
        (requestData) => {
            axios
                .post(`/api/hospitals/${hospital.id}`, requestData)
                .then(() => {
                    doFetch();
                })
                .catch((error) => {
                    window.alert("something went wrong! try again.");
                });
        },
        [hospital, doFetch]
    );

    return (
        <div className="w-full max-w-md shadow-lg m-auto">
            <h1 className="text-lg p-4 text-center font-bold bg-gray-200 uppercase">
                {"Hospital Details"}
            </h1>
            <div className="flex flex-col p-6">
                <form onSubmit={handleSubmit(onSubmit)}>
                    <input
                        type="text"
                        disabled={!editable}
                        name="id"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300 hidden"
                    />
                    <input
                        type="text"
                        disabled={!editable}
                        placeholder="Name"
                        name="name"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <input
                        type="tel"
                        disabled={!editable}
                        placeholder="Mobile Number"
                        name="mobileNumber"
                        ref={register({
                            required: true,
                            minLength: 6,
                            maxLength: 12,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <input
                        type="text"
                        disabled={!editable}
                        placeholder="Street"
                        name="address.street"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <div className="flex flex-row">
                        <input
                            type="text"
                            disabled={!editable}
                            placeholder="City"
                            name="address.city"
                            ref={register({
                                required: true,
                                maxLength: 80,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                        />
                        <input
                            type="text"
                            disabled={!editable}
                            placeholder="State"
                            name="address.state"
                            ref={register({
                                required: true,
                                maxLength: 50,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                        />
                        <input
                            type="text"
                            disabled={!editable}
                            placeholder="ZipCode"
                            name="address.zipCode"
                            ref={register({
                                required: true,
                                maxLength: 6,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 "
                        />
                    </div>
                    {editable && (
                        <div className="flex justify-end">
                            <button
                                type="submit"
                                disabled={editable}
                                className="text-white bg-gray-500 px-3 py-2"
                            >
                                {"Update"}
                            </button>
                        </div>
                    )}
                </form>
            </div>
        </div>
    );
};

const FetchHospital = ({ id, data, updateData, ...props }) => {
    const { loading, doFetch } = useFetch({
        url: `/api/hospitals/${id}`,
        onComplete: updateData,
    });

    useEffect(() => {
        if (!id || data) {
            return;
        }
        doFetch();
    }, [id, data, doFetch]);

    if (loading || !data) {
        return null;
    }

    return <Hospital {...props} hospital={data} doFetch={doFetch} />;
};

export default FetchHospital;
