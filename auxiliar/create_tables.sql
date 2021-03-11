CREATE TABLE jl_metrics (
            id uuid,
            device_id varchar(50),
            timestamp timestamp,
            metric_name varchar(50),
            metric_value double precision
            );

            create view vw_jl_metrics as
	        select device_id || metric_name as metric_name, timestamp, metric_value from jl_metrics;
            
            CREATE TABLE other_metrics (
            id uuid,
            device_id varchar(50),
            timestamp timestamp,
            metric_name varchar(50),
            metric_value double precision
            );

            create view vw_other_metrics as
	        select device_id || metric_name as metric_name, timestamp, metric_value from other_metrics;
            
            