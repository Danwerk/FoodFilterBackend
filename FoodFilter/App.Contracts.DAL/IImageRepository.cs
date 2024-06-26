﻿using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IImageRepository: IBaseRepository<Image>
{
    public Image Add(Image entity);

}