## Influxdb

    ```

SELECT mean(value) FROM "temperature"
WHERE ("datatype" = 'TMAX')
GROUP BY dayofyear

CREATE CONTINUOUS QUERY "cq_basic" ON "transportation"
BEGIN
SELECT mean("passengers") INTO "average_passengers" FROM "bus_data" GROUP BY time(1h)
END

CREATE CONTINUOUS QUERY "cq_basic" ON "weather"
BEGIN
SELECT mean(value) INTO "average_temperature" FROM "temperature"
WHERE ("datatype" = 'TMAX')
GROUP BY dayofyear
END

    ```



    ```
    [
        {
            "mindate": "1982-01-01",
            "maxdate": "2019-12-22",
            "name": "Average wind speed",
            "datacoverage": 1,
            "id": "AWND"
        },
        {
            "mindate": "1948-01-01",
            "maxdate": "2019-12-15",
            "name": "Peak gust time",
            "datacoverage": 1,
            "id": "PGTM"
        },
        {
            "mindate": "1781-01-01",
            "maxdate": "2019-12-23",
            "name": "Precipitation",
            "datacoverage": 1,
            "id": "PRCP"
        },
        {
            "mindate": "1840-05-01",
            "maxdate": "2019-12-23",
            "name": "Snowfall",
            "datacoverage": 1,
            "id": "SNOW"
        },
        {
            "mindate": "1857-01-18",
            "maxdate": "2019-12-23",
            "name": "Snow depth",
            "datacoverage": 1,
            "id": "SNWD"
        },
        {
            "mindate": "1763-01-01",
            "maxdate": "2019-12-24",
            "name": "Maximum temperature",
            "datacoverage": 1,
            "id": "TMAX"
        },
        {
            "mindate": "1763-01-01",
            "maxdate": "2019-12-24",
            "name": "Minimum temperature",
            "datacoverage": 1,
            "id": "TMIN"
        },
        {
            "mindate": "1965-01-01",
            "maxdate": "2019-12-14",
            "name": "Total sunshine for the period",
            "datacoverage": 1,
            "id": "TSUN"
        },
        {
            "mindate": "1993-06-01",
            "maxdate": "2019-12-15",
            "name": "Direction of fastest 2-minute wind",
            "datacoverage": 1,
            "id": "WDF2"
        },
        {
            "mindate": "1993-06-01",
            "maxdate": "2019-12-15",
            "name": "Direction of fastest 5-second wind",
            "datacoverage": 1,
            "id": "WDF5"
        },
        {
            "mindate": "1993-06-01",
            "maxdate": "2019-12-15",
            "name": "Fastest 2-minute wind speed",
            "datacoverage": 1,
            "id": "WSF2"
        },
        {
            "mindate": "1993-06-01",
            "maxdate": "2019-12-15",
            "name": "Fastest 5-second wind speed",
            "datacoverage": 1,
            "id": "WSF5"
        },
        {
            "mindate": "1851-05-19",
            "maxdate": "2019-12-15",
            "name": "Fog, ice fog, or freezing fog (may include heavy fog)",
            "datacoverage": 1,
            "id": "WT01"
        },



        {
            "mindate": "1851-04-11",
            "maxdate": "2019-12-15",
            "name": "Smoke or haze ",
            "datacoverage": 1,
            "id": "WT08"
        }

    ]
    ```
