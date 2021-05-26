	SELECT d.d as [Дата],
		s.store_name as [Аптека],
		g.group_name as [Группа товара],
		g.good_name as [Номенклатура],
		sum(f.quantity) as [Продажи шт.],
		sum(f.sale_grs) as [Продажи руб., с НДС],
		sum(f.cost_grs) as [Закупка руб., с НДС],
		(sum(f.sale_grs)/(select sum(sale_grs) 
							from [dbo].[fct_cheque] as f
							inner join dim_goods as g
								on g.good_id = f.good_id
							inner join dim_date as d
								on d.did = f.date_id
							inner join [dim_stores] as s
								on s.store_id = f.store_id
							inner join dbo.dim_cash_register as cr
								on cr.cash_register_id = f.cash_register_id
							join string_split(@good_group_name, ',')
								on value = g.group_name
							where g.good_id = f.good_id and 
							date_id between @date_from_int and @date_to_int 							
									))*100 as [Доля продаж с НДС],
		(sum(f.cost_net)/nullif(sum(f.quantity),0)) as [Средняя цена закупки руб., без НДС],
		(sum(f.sale_net) - sum(f.cost_net))   as [Маржа руб. без НДС],
		((sum(f.sale_net) - sum(f.cost_net))/(nullif(sum(f.cost_net),0)))*100 as [Наценка % без НДС]		
	FROM [dbo].[fct_cheque] as f
	inner join dim_goods as g
		on g.good_id = f.good_id
	inner join [dim_stores] as s
		on s.store_id = f.store_id
	inner join dim_date as d
		on d.did = f.date_id
	inner join dbo.dim_cash_register as cr
		on cr.cash_register_id = f.cash_register_id	
	join STRING_SPLIT (@good_group_name, ',')
		on value = g.group_name
	where date_id between @date_from_int and @date_to_int	
	group by d.d,
		s.store_name,
		g.group_name,
		g.good_name